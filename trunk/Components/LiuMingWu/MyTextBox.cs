using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Web.UI;
using System.ComponentModel.Design;
using System.Web.UI.WebControls;
using System.Collections;

namespace BP.GE.Ctrl
{
    public enum style
    {
        style1,
        style2
    }

    [ToolboxData("<{0}:MyTextBox runat='server'></{0}:MyTextBox>")]
    [Designer(typeof(TextBoxDesigner))]
    public class MyTextBox : Control
    {

        /// <summary>
        /// Windows消息，窗体创建的消息。
        /// </summary>
        private const int WM_CREATE = 0x0001;
        private string _strText;
        private int _width;
        private bool _display;
        private style _style;
        Button button = new Button();
        /// <summary>
        /// 显示在智能标签的属性
        /// </summary>
        public string strText
        {
            get
            {
                return _strText;
            }
            set
            {
                _strText = value;
            }
        }
        /// <summary>
        /// 显示在智能标签的属性
        /// </summary>
        public int width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
            }
        }
        public bool Display
        {
            get
            {
                return _display;
            }
            set
            {
                _display = value;
            }
        }
        public style style
        {
            get
            {
                return _style;
            }
            set
            {
                _style = value;
            }
        }

        public MyTextBox()
        {

        }
        public void testMethod()
        {

        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.Write("你好!" + strText + "," + width.ToString() + "," + Display.ToString());
            if (this.style == style.style1)
            {
                writer.WriteBreak();
                writer.Write("您选择了样式一");
            }
            else
            {
                writer.WriteBreak();
                writer.Write("您选择了样式二");
            }
            for (int i = 0; i < width; i++)
            {
                button = new Button();
                button.Text = "Button " + i.ToString();
                this.Controls.Add(button);
                button.RenderControl(writer);
            }
        }
    }
    public class MyTextBoxActionList : DesignerActionList
    {
        private MyTextBox _textBox;
        private DesignerActionUIService designerActionUISvc = null;
        //The constructor associates the control 
        //with the smart tag list
        public MyTextBoxActionList(IComponent component)
            : base(component)
        {
            this._textBox = component as MyTextBox;

            // Cache a reference to DesignerActionUIService, 
            // so the DesigneractionList can be refreshed.
            this.designerActionUISvc = GetService(typeof(DesignerActionUIService)) as DesignerActionUIService;

        }

        // Helper method to retrieve control properties. Use of 
        // GetProperties enables undo and menu updates to work properly.
        private PropertyDescriptor GetPropertyByName(String propName)
        {
            PropertyDescriptor prop;
            prop = TypeDescriptor.GetProperties(_textBox)[propName];
            if (null == prop)
                throw new ArgumentException(
                     "Matching ColorLabel property not found!",
                      propName);
            else
                return prop;
        }
        // Method that is target of a DesignerActionMethodItem
        public void InvertColors()
        {
            //Color currentBackColor = colLabel.BackColor;
            //BackColor = Color.FromArgb(
            //    255 - currentBackColor.R,
            //    255 - currentBackColor.G,
            //    255 - currentBackColor.B);

            //Color currentForeColor = colLabel.ForeColor;
            //ForeColor = Color.FromArgb(
            //    255 - currentForeColor.R,
            //    255 - currentForeColor.G,
            //    255 - currentForeColor.B);
        }


        /// <summary>
        /// 该控件的属性
        /// </summary>
        public string strText
        {
            get
            {
                return _textBox.strText;
            }
            set
            {
                GetPropertyByName("strText").SetValue(_textBox, value);

            }
        }
        /// <summary>
        /// 该控件的属性
        /// </summary>
        public int width
        {
            get
            {
                return _textBox.width;
            }
            set
            {
                GetPropertyByName("width").SetValue(_textBox, value);
            }
        }
        /// <summary>
        /// 控件的属性
        /// </summary>
        public bool display
        {
            get
            {
                return _textBox.Display;
            }
            set
            {
                GetPropertyByName("Display").SetValue(_textBox, value);
            }

        }
        /// <summary>
        /// 测试枚举属性
        /// </summary>
        public style style
        {
            get
            {
                return _textBox.style;
            }
            set
            {
                GetPropertyByName("style").SetValue(_textBox, value);
            }
        }
        /// <summary>
        /// 重写父类的方法，实现在智能标签显示的属性
        /// </summary>
        /// <returns></returns>
        public override DesignerActionItemCollection GetSortedActionItems()
        {

            DesignerActionItemCollection items = new DesignerActionItemCollection();

            items.Add(new DesignerActionHeaderItem("Appearance"));

            items.Add(new DesignerActionPropertyItem("strText", "标题"));
            items.Add(new DesignerActionPropertyItem("width", "宽度"));
            items.Add(new DesignerActionPropertyItem("display", "可见"));
            items.Add(new DesignerActionPropertyItem("style", "样式"));

            //Create entries for static Information section.
            StringBuilder location = new StringBuilder("Location: ");
            location.Append("colLabel.Location");
            StringBuilder size = new StringBuilder("Size: "); size.Append("colLabel.Size");
            items.Add(new DesignerActionTextItem(location.ToString(), "Information"));
            items.Add(new DesignerActionTextItem(size.ToString(), "Information"));
            items.Add(new DesignerActionMethodItem(this, "Test", "测试方法"));
            return items;
        }

        public void Test()
        {
            GetPropertyByName("strText").SetValue(_textBox, "这是我的测试数据");
            //designerActionUISvc.Refresh(Component);
            designerActionUISvc.HideUI(this.Component);
        }
    }

    public class TextBoxDesigner : System.Web.UI.Design.ControlDesigner
    {
        // Use pull model to populate smart tag menu.
        private DesignerActionListCollection _actionLists;
        public override System.ComponentModel.Design.DesignerActionListCollection ActionLists
        {
            get
            {
                if (_actionLists == null)
                {
                    _actionLists = new DesignerActionListCollection();
                    _actionLists.Add(new MyTextBoxActionList(this.Component));
                }
                return _actionLists;
            }
        }
    }
}
