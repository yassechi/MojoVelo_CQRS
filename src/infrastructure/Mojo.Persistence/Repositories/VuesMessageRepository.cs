using Microsoft.EntityFrameworkCore;
using Mojo.Application.Persistance.Contracts;
using Mojo.Domain.Entities;
using Mojo.Persistence.DatabaseContext;

namespace Mojo.Persistence.Repositories
{
    public class VuesMessageRepository : GenericRepository<VuesMessage>, IVuesMessageRepository
    {
        private readonly MDbContext _db;

        public VuesMessageRepository(MDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<int> GetUnreadCountAsync(string userId, int role, int? organisationId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return 0;
            }

            var demandesQuery = _db.Set<Demande>()
                .AsNoTracking()
                .Where(d => d.IsActif);

            if (role == 2)
            {
                if (!organisationId.HasValue)
                {
                    return 0;
                }
                demandesQuery = demandesQuery.Where(d => d.User.OrganisationId == organisationId.Value);
            }
            else if (role == 3)
            {
                demandesQuery = demandesQuery.Where(d => d.IdUser == userId);
            }

            var messagesQuery = from m in _db.Set<Message>().AsNoTracking()
                                join d in demandesQuery on m.DiscussionId equals d.DiscussionId
                                where m.IsActif
                                select m;

            messagesQuery = messagesQuery.Where(m => m.CreatedBy != userId);

            var viewsQuery = _db.Set<VuesMessage>()
                .AsNoTracking()
                .Where(v => v.UserId == userId);

            var count = await (
                from m in messagesQuery
                join v in viewsQuery on m.Id equals v.MessageId into views
                from v in views.DefaultIfEmpty()
                where v == null
                select m.Id
            ).CountAsync();

            return count;
        }

        public async Task<List<int>> GetUnreadDiscussionIdsAsync(string userId, int role, int? organisationId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return new List<int>();
            }

            var demandesQuery = _db.Set<Demande>()
                .AsNoTracking()
                .Where(d => d.IsActif && d.DiscussionId > 0);

            if (role == 2)
            {
                if (!organisationId.HasValue)
                {
                    return new List<int>();
                }
                demandesQuery = demandesQuery.Where(d => d.User.OrganisationId == organisationId.Value);
            }
            else if (role == 3)
            {
                demandesQuery = demandesQuery.Where(d => d.IdUser == userId);
            }

            var messagesQuery = from m in _db.Set<Message>().AsNoTracking()
                                join d in demandesQuery on m.DiscussionId equals d.DiscussionId
                                where m.IsActif && m.DiscussionId > 0
                                select new { m.Id, m.DiscussionId, m.CreatedBy };

            messagesQuery = messagesQuery.Where(m => m.CreatedBy != userId);

            var viewsQuery = _db.Set<VuesMessage>()
                .AsNoTracking()
                .Where(v => v.UserId == userId);

            var discussionIds = await (
                from m in messagesQuery
                join v in viewsQuery on m.Id equals v.MessageId into views
                from v in views.DefaultIfEmpty()
                where v == null
                select m.DiscussionId
            ).Distinct().ToListAsync();

            return discussionIds;
        }

        public async Task<int> MarkMessagesAsReadAsync(string userId, IEnumerable<int> messageIds)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return 0;
            }

            var ids = messageIds?
                .Where(id => id > 0)
                .Distinct()
                .ToList() ?? new List<int>();

            if (ids.Count == 0)
            {
                return 0;
            }

            var now = DateTime.Now;
            var existing = await _db.Set<VuesMessage>()
                .Where(v => v.UserId == userId && ids.Contains(v.MessageId))
                .ToListAsync();

            var existingIds = existing.Select(v => v.MessageId).ToHashSet();

            foreach (var view in existing)
            {
                view.ViewedAt = now;
                view.ModifiedBy = userId;
            }

            var newViews = ids
                .Where(id => !existingIds.Contains(id))
                .Select(id => new VuesMessage
                {
                    UserId = userId,
                    MessageId = id,
                    ViewedAt = now,
                    CreatedBy = userId,
                    ModifiedBy = userId,
                    IsActif = true
                })
                .ToList();

            if (newViews.Count > 0)
            {
                await _db.Set<VuesMessage>().AddRangeAsync(newViews);
            }

            await _db.SaveChangesAsync();
            return existing.Count + newViews.Count;
        }
    }
}
