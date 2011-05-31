using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Interactivity;
using Microsoft.Expression.Interactivity;
using Microsoft.Expression.Interactivity.Layout;


namespace Demo
{
    public partial class DrogTest : UserControl
    {
        public DrogTest()
        {
            InitializeComponent();

            //用程式套用MouseDragElementBehavior到物件上 .
            MouseDragElementBehavior DragBehavior = new MouseDragElementBehavior();
           Interaction.GetBehaviors(TestObject).Add(DragBehavior);

            //Interaction.GetBehaviors(checkBox1).Add(DragBehavior);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            //一般情況使用的GetValue()方法，無法正確取得套用MouseDragElementBehavior物件移動後的值  

            LeftValue.Text = "Left值:" + TestObject.GetValue(Canvas.LeftProperty).ToString();
            TopValue.Text = "Top值:" + TestObject.GetValue(Canvas.TopProperty).ToString();

            //取得MouseDragElementBehavior的X與Y值，可以正確取得物件移動後的值  
            MouseDragElementBehavior m1 = Interaction.GetBehaviors(TestObject)[0] as MouseDragElementBehavior;

            LeftValue1.Text = "Left值:" + m1.X.ToString();
            TopValue1.Text = "Top值:" + m1.Y.ToString();
        }
    }
}
