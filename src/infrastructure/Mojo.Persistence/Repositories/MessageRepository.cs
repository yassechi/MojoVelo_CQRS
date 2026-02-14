using Microsoft.EntityFrameworkCore;
using Mojo.Application.Features.Messages.Request.Query;
using Mojo.Application.Persistance.Contracts;
using Mojo.Domain.Entities;
using Mojo.Persistence.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mojo.Persistence.Repositories
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        private readonly MDbContext _db;
        public MessageRepository(MDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<Message>> GetByDiscussionId(int discussionId)
        {
            return await _db.Set<Message>()
                .Where(m => m.DiscussionId == discussionId)
                .OrderBy(m => m.DateEnvoi)
                .ToListAsync();
        }

    }
}
