using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using prismic;
using prismic.fragments;

namespace LilyAbramSite.Prismic.Helpers
{
    public class PrismicLinkResolver : DocumentLinkResolver
    {
        public override string Resolve(DocumentLink link)
        {
            return $"/project/{link.Uid}";
        }
    }
}
