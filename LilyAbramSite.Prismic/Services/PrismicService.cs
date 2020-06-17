using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using prismic;
using Microsoft.Extensions.Logging;
using LilyAbramSite.Prismic.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using LilyAbramSite.Prismic.Helpers;

namespace LilyAbramSite.Prismic.Services
{
    public class PrismicService : IPrismicService
    {
        protected ILogger<PrismicService> Logger { get; }
        protected string ApiUrl { get; set; }
        public IPrismicApiAccessor ApiAccessor { get; }

        private List<Project> _projects;

        public PrismicService(ILogger<PrismicService> logger, IConfiguration configuration, IPrismicApiAccessor apiAccessor)
        {
            Logger = logger;
            ApiUrl = configuration.GetSection("Prismic:Url").Value;
            ApiAccessor = apiAccessor;
        }

        private Api _api;
        private async ValueTask<Api> GetApi()
        {
            if(_api == null)
            {
                _api = await ApiAccessor.GetApi(ApiUrl);
            }

            return _api;
        }

        protected async Task<Document> GetSingle(string documentType)
        {
            var api = await GetApi();
            return await api.QueryFirst(Predicates.At("document.type", documentType));
        }

        public async Task<Project> GetProject(string uid)
        {
            return (await GetProjects()).FirstOrDefault(x => x.Uid == uid);
        }

        public async Task<IList<Project>> GetProjects()
        {
            if(_projects == null)
            {
                var api = await GetApi();
                var response = await api.Query(Predicates.At("document.type", "project")).PageSize(100).Submit();
                _projects = response.Results.Select(x => x.ToProject()).OrderBy(x => x.Order).ToList();
            }

            return _projects;
        }

        public async Task<Globals> GetGlobals()
        {
            var globalDoc = await GetSingle("globals");

            return new Globals()
            {
                SiteName = globalDoc.GetText("globals.site_name"),
                About = globalDoc.GetHtml("globals.about", new Helpers.PrismicLinkResolver()),
                Contact = globalDoc.GetHtml("globals.contact", new Helpers.PrismicLinkResolver()),
            };          
        }

    }
}
