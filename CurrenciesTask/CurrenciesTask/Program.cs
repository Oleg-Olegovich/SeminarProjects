using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Newtonsoft.Json;
using Npgsql;
using RestSharp;

namespace CurrenciesTask
{
    class Program
    {
        private const string ConnectionString =
            "Host=139.28.223.173;Port=5432;User Id=oomanzhula;Password=oomanzhula;Database=oomanzhula_db;";

        static async Task Main(string[] args)
        {
            using var db = new NpgsqlConnection(ConnectionString);
            await CreateTable(db);
            foreach (var currency in await GetCurrenciesExchangeRate())
            {
                Console.WriteLine(currency);
                await Write(db, currency);
            }
        }

        static async Task Query(NpgsqlConnection db, string query)
            => await db.QueryAsync(new CommandDefinition(query));

        static async Task CreateSchema(NpgsqlConnection db)
            => await Query(db, "CREATE SCHEMA IF NOT EXISTS crm;");

        static async Task CreateTable(NpgsqlConnection db)
        {
            await CreateSchema(db);
            await DropTable(db);
            var query =
                @"CREATE TABLE IF NOT EXISTS crm.rates(
                   id          SERIAL,
                   eur         DECIMAL    NOT NULL,
                   usd         DECIMAL    NOT NULL,
                   jpy         DECIMAL    NOT NULL,
                   date        TEXT       NOT NULL
                );";
            await Query(db, query);
        }

        static async Task DropTable(NpgsqlConnection db)
            => await Query(db, "DROP TABLE IF EXISTS crm.rates;");

        static async Task<Currency> GetCurrenciesExchangeRate(DateTime date, int id)
        {
            var queryDate = date.Year + "-" + date.Month + "-" + date.Day;
            var client = new RestClient("https://api.ratesapi.io/api/");
            var request = new RestRequest(queryDate + "?base=RUB", DataFormat.Json);
            var response = client.Get(request);
            var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(response.Content);
            var currencies = JsonConvert.DeserializeObject<Dictionary<string, object>>(data["rates"].ToString());
            var result = new Currency()
            {
                Dollar = 1 / decimal.Parse(currencies["USD"].ToString()),
                Euro = 1 / decimal.Parse(currencies["EUR"].ToString()),
                Yen = 1 / decimal.Parse(currencies["JPY"].ToString()),
                Date = date,
                Id = id
            };
            return result;
        }

        static async Task<List<Currency>> GetCurrenciesExchangeRate()
        {
            var today = DateTime.Today;
            int id = 0;
            var result = new List<Currency>();
            for (var date = new DateTime(2020, 1, 1); date < today; date = date.AddDays(1))
            {
                result.Add(await GetCurrenciesExchangeRate(date, id++));
            }
            return result;
        }

        static async Task Write(NpgsqlConnection db, Currency currency)
        {
            var euro = currency.Euro;
            var usd = currency.Dollar;
            var yen = currency.Yen;
            var date = currency.Date;
            await db.QueryAsync($"insert into crm.rates (eur,usd,jpy,date) values (@eur,@usd,@jpy,@date);",
                new { eur = euro, usd = usd, jpy = yen, date = date });
        }
    }

    class Currency
    {
        public decimal Dollar { get; set; }

        public decimal Euro { get; set; }

        public decimal Yen { get; set; }

        public int Id { get; set; }

        public DateTime Date { get; set; }

        public override string ToString()
        {
            return $"{Id}: date:{Date}, USD:{Dollar}, EUR:{Euro}, JPY:{Yen}";
        }
    }
}