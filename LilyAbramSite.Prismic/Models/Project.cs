using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LilyAbramSite.Prismic.Models
{
    public class Project
    {
        public string Uid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public IList<string> ImageUrls { get; set; }
        public string ThumbnailUrl { get; set; }
        public string ThumbnailClass { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
    }

    public static class ProjectExtensions
    {
        public static Project ToProject(this prismic.Document doc)
        {
            return new Project()
            {
                Uid = doc.Uid,
                Name = doc.GetText($"{doc.Type}.name"),
                Description = doc.GetHtml($"{doc.Type}.description", new Helpers.PrismicLinkResolver()),
                Order = Convert.ToInt32(doc.GetNumber($"{doc.Type}.order")?.Value),
                ImageUrls = doc.GetGroup($"{doc.Type}.images").GroupDocs.Select(x => x.GetImageView("image", "main").Url).ToList(),
                ThumbnailUrl = doc.GetImage($"{doc.Type}.thumbnail").GetView("main").Url,
                ThumbnailClass = doc.GetText($"{doc.Type}.thumbnail_display"),
                MetaTitle = doc.GetText($"{doc.Type}.meta_title"),
                MetaDescription = doc.GetText($"{doc.Type}.meta_description")
            };
        }
    }
}
