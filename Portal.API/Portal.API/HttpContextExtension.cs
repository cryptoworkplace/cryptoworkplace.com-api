using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.API
{
    public static class HttpContextExtensions
    {
        public static (IOutputFormatter SelectedFormatter, OutputFormatterWriteContext FormatterContext) SelectFormatter<TModel>(this HttpContext context, TModel model)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (model == null) throw new ArgumentNullException(nameof(model));

            var selector = context.RequestServices.GetRequiredService<OutputFormatterSelector>();
            var writerFactory = context.RequestServices.GetRequiredService<IHttpResponseStreamWriterFactory>();
            var formatterContext = new OutputFormatterWriteContext(context, writerFactory.CreateWriter, typeof(TModel), model);

            var selectedFormatter = selector.SelectFormatter(formatterContext, Array.Empty<IOutputFormatter>(), new MediaTypeCollection());
            return (selectedFormatter, formatterContext);
        }
    }
}
