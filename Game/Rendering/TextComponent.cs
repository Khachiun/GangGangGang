using Czaplicki.SFMLE;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GangGang
{
    class TextComponent : DrawComponent
    {
        Text text;
        public string Text
        {
            get { return text.DisplayedString; }
            set { text.DisplayedString = value; }
        }
        private static int Count { get; set; }
        public TextComponent(string text, int layer) : base("", layer)
        {
            base.ID = CreateRandomID();
            this.text = DefaultText.GenerateText(text);
            base.singleUseDrawble = true;
            Regiser(this.ID, this.text);
        }
        
    }
}
