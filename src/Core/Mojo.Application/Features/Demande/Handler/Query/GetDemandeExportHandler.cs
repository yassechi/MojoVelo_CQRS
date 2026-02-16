using System.Globalization;
using System.Text;
using Mojo.Application.DTOs.EntitiesDto.Demande;
using Mojo.Application.Features.Demandes.Request.Query;
using Mojo.Domain.Enums;

namespace Mojo.Application.Features.Demandes.Handlers.Query
{
    public class GetDemandeExportHandler : IRequestHandler<GetDemandeExportRequest, string>
    {
        private readonly IMediator _mediator;
        private static readonly CultureInfo FrCulture = CultureInfo.GetCultureInfo("fr-FR");

        public GetDemandeExportHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<string> Handle(GetDemandeExportRequest request, CancellationToken cancellationToken)
        {
            var demandes = await _mediator.Send(new GetDemandeListRequest
            {
                Status = request.Status,
                Type = request.Type,
                Search = request.Search,
                OrganisationId = request.OrganisationId,
                UserId = request.UserId
            }, cancellationToken);

            var headers = new[] { "ID", "Employe", "Velo", "Type", "Prix", "Statut" };
            var sb = new StringBuilder();
            sb.AppendLine(ToCsvRow(headers));

            foreach (var demande in demandes)
            {
                var row = new[]
                {
                    demande.Id.ToString(CultureInfo.InvariantCulture),
                    demande.UserName,
                    $"{demande.VeloMarque} {demande.VeloModele}".Trim(),
                    demande.VeloType ?? string.Empty,
                    FormatCurrency(demande.VeloPrixAchat),
                    GetStatusLabel(demande.Status)
                };
                sb.AppendLine(ToCsvRow(row));
            }

            return sb.ToString();
        }

        private static string FormatCurrency(decimal? amount)
        {
            if (!amount.HasValue)
            {
                return string.Empty;
            }
            return string.Format(FrCulture, "{0:C}", amount.Value);
        }

        private static string GetStatusLabel(DemandeStatus status)
        {
            return status switch
            {
                DemandeStatus.Encours => "En cours",
                DemandeStatus.AttenteComagnie => "Attente Compagnie",
                DemandeStatus.Finalisation => "Finalisation",
                DemandeStatus.Valide => "Valide",
                DemandeStatus.Refusé => "Refus\u00e9",
                _ => "Inconnu"
            };
        }

        private static string ToCsvRow(IEnumerable<string?> cells)
        {
            return string.Join(";", cells.Select(EscapeCell));
        }

        private static string EscapeCell(string? value)
        {
            var raw = value ?? string.Empty;
            var escaped = raw.Replace("\"", "\"\"");
            return $"\"{escaped}\"";
        }
    }
}
