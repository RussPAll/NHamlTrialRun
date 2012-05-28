using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace NHamlTrialRun.NHamlImpl
{
    public abstract class NHamlView<T> : NHaml4.TemplateBase.TypedTemplate<T>
    {
        public string Render()
        {
            var text = "";
            using (var writer = new StringWriter())
            {
                base.Render(writer);
                text = writer.ToString();
            }
            return text;
        }

        public override string ToString()
        {
            return Render();
        }
    }
    
    public class NHamlLayout : NHamlView<object>
    {
        public NHamlLayout()
        {
        }

        public object Content { get; set; }
    }
}
