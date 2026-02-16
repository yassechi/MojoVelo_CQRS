using System.Globalization;
using System.Text;
using Mojo.Application.Features.Contrats.Request.Query;
using Mojo.Domain.Enums;

namespace Mojo.Application.Features.Contrats.Handler.Query
{
    public class GetContratExportHandler : IRequestHandler<GetContratExportRequest, string>
    {
        private readonly IMediator _mediator;
        private static readonly CultureInfo FrCulture = CultureInfo.GetCultureInfo("fr-FR");

        public GetContratExportHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<string> Handle(GetContratExportRequest request, CancellationToken cancellationToken)
        {
            var contrats = await _mediator.Send(new GetContratListRequest
            {
                Type = request.Type,
                Search = request.Search,
                EndingSoon = request.EndingSoon,
                WithIncidents = request.WithIncidents,
                OrganisationId = request.OrganisationId,
                UserId = request.UserId
            }, cancellationToken);

            var headers = new[] { "Reference", "Employe", "Velo", "Debut", "Fin", "Statut" };
            var sb = new StringBuilder();
            sb.AppendLine(ToCsvRow(headers));

            foreach (var contrat in contrats)
            {
                var row = new[]
                {
                    contrat.Ref,
                    contrat.BeneficiaireName,
                    $"{contrat.VeloMarque} {contrat.VeloModele}".Trim(),
                    FormatDate(contrat.DateDebut),
                    FormatDate(contrat.DateFin),
                    GetStatutLabel(contrat.StatutContrat)
                };
                sb.AppendLine(ToCsvRow(row));
            }

            return sb.ToString();
        }

        private static string FormatDate(DateOnly date)
        {
            return date.ToString("d", FrCulture);
        }

        private static string GetStatutLabel(StatutContrat statut)
        {
            return statut switch
            {
                StatutContrat.EnCours => "En cours",
                StatutContrat.Termine => "Termin\u00e9",
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
