using Kino.API.Models;
using Kino.API.Models.ServiceResponse;
using Kino.Models.KinoDBModel;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Kino.API.Helpers
{
    public static class ServiceHelper
    {
        public static Draw GetDrawFromOPAP()
        {
            var client = new RestClient(ConfigurationHelper.GetConfiguration().GetSection("OPAPServiceBaseURL").Value);
            var request = new RestRequest(OPAPGames.Kino + "/last-result-and-active");
            var response = client.Execute(request, Method.GET);

            var resp = JsonConvert.DeserializeObject<Root>(response.Content);

            var winningNumbers = string.Empty;

            resp.last.winningNumbers.list.ForEach(number =>
            {
                winningNumbers += number.ToString();
                if (resp.last.winningNumbers.list.Last() != number)
                {
                    winningNumbers += "#";
                }
            });

            return new Draw
            {
                DrawId = resp.last.drawId.ToString(),
                DrawDateTime = new DateTime(1970, 1, 1).AddMilliseconds(resp.last.drawTime).AddHours(3),
                WinningNumbers = winningNumbers,
                KinoBonus = resp.last.winningNumbers.bonus.FirstOrDefault().ToString()
            };
        }
    }
}
