using BlazorDataBinding.Shared;
using C1.DataCollection;
using C1.DataCollection.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace BlazorDataBinding.Client.Models
{
    public class CustomerVirtualDataCollection : C1VirtualDataCollection<Customers>
    {
        public HttpClient Http { get; set; }
        private static readonly string URL = "api/customer";
        public override bool CanSort(params SortDescription[] sortDescriptions)
        {
            return true;
        }

        public override bool CanFilter(FilterExpression filterExpression)
        {
            return !(filterExpression is FilterPredicateExpression);
        }

        protected override async Task<Tuple<int, IReadOnlyList<Customers>>> GetPageAsync(int pageIndex, int startingIndex, int count, IReadOnlyList<SortDescription> sortDescriptions = null, FilterExpression filterExpression = null, CancellationToken cancellationToken = default)
        {
            string url = $"{URL}?skip={startingIndex}&take={count}";
            if (sortDescriptions?.Count > 0)
            {
                url += $"&sort={Uri.EscapeUriString(JsonSerializer.Serialize<IReadOnlyList<SortDescription>>(sortDescriptions))}";
            }
            if (filterExpression != null)
            {
                var options = new JsonSerializerOptions { Converters = { new FilterExpressionJsonConverter() } };
                url += $"&filter={Uri.EscapeUriString(JsonSerializer.Serialize(filterExpression, options))}";
            }
            var response = await Http.GetFromJsonAsync<CustomerResponse>(new Uri(url, UriKind.Relative), cancellationToken);
            return new Tuple<int, IReadOnlyList<Customers>>(response.TotalCount, response.Customers.ToList());
            
        }
    }
}
