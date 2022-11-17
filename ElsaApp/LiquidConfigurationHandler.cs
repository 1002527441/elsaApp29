using Elsa.Scripting.Liquid.Messages;
using ElsaApp.Data;
using Fluid;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ElsaApp
{
    public class LiquidConfigurationHandler : INotificationHandler<EvaluatingLiquidExpression>
    {
        public Task Handle(EvaluatingLiquidExpression notification, CancellationToken cancellationToken)
        {
            var strategy = notification.TemplateContext.Options.MemberAccessStrategy;

            strategy.Register<DTOHello>();

         


            return Task.CompletedTask;
        }
    }
}
