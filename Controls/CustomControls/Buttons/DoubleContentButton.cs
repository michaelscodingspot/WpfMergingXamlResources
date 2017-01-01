using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Controls.CustomControls.Buttons
{
    
    public class DoubleContentButton : Button
    {
        static DoubleContentButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DoubleContentButton), new FrameworkPropertyMetadata(typeof(DoubleContentButton)));
        }



        public object FirstContent
        {
            get { return (object)GetValue(FirstContentProperty); }
            set { SetValue(FirstContentProperty, value); }
        }
        public static readonly DependencyProperty FirstContentProperty =
            DependencyProperty.Register("FirstContent", typeof(object), typeof(DoubleContentButton), new PropertyMetadata(null));


        public object SecondContent
        {
            get { return (object)GetValue(SecondContentProperty); }
            set { SetValue(SecondContentProperty, value); }
        }
        public static readonly DependencyProperty SecondContentProperty =
            DependencyProperty.Register("SecondContent", typeof(object), typeof(DoubleContentButton), new PropertyMetadata(null));






    }





    



}
