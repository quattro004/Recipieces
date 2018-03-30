using System.Collections.Generic;
using System.Reflection;
using codeAnalysis = Microsoft.CodeAnalysis;

namespace Recipieces.FeaturesPlugin.ViewModels
{
    public class FeaturesViewModel
    {
        public List<TypeInfo> Controllers { get; set; }

        public List<codeAnalysis.MetadataReference> MetadataReferences { get; set; }

        public List<TypeInfo> TagHelpers { get; set; }

        public List<TypeInfo> ViewComponents { get; set; } 
    }
}