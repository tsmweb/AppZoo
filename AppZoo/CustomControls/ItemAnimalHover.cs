using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234235

namespace AppZoo.CustomControls
{
    public sealed class ItemAnimalHover : Control
    {
        public string Especie
        {
            get { return (string)GetValue(EspecieProperty); }
            set { SetValue(EspecieProperty, value); }
        }

        public static readonly DependencyProperty EspecieProperty =
            DependencyProperty.Register("Especie", typeof(string), typeof(ItemAnimalHover), new PropertyMetadata(null));

        public string Nome
        {
            get { return (string)GetValue(NomeProperty); }
            set { SetValue(NomeProperty, value); }
        }

        public static readonly DependencyProperty NomeProperty =
            DependencyProperty.Register("Nome", typeof(string), typeof(ItemAnimalHover), new PropertyMetadata(null));

        public string Tamanho
        {
            get { return (string)GetValue(TamanhoProperty); }
            set { SetValue(TamanhoProperty, value); }
        }

        public static readonly DependencyProperty TamanhoProperty =
            DependencyProperty.Register("Tamanho", typeof(string), typeof(ItemAnimalHover), new PropertyMetadata(null));

        public string Sexo
        {
            get { return (string)GetValue(SexoProperty); }
            set { SetValue(SexoProperty, value); }
        }

        public static readonly DependencyProperty SexoProperty =
            DependencyProperty.Register("Sexo", typeof(string), typeof(ItemAnimalHover), new PropertyMetadata(null));

        public string Cor
        {
            get { return (string)GetValue(CorProperty); }
            set { SetValue(CorProperty, value); }
        }

        public static readonly DependencyProperty CorProperty =
            DependencyProperty.Register("Cor", typeof(string), typeof(ItemAnimalHover), new PropertyMetadata(null));

        public string NomeImagem
        {
            get { return (string)GetValue(NomeImagemProperty); }
            set { SetValue(NomeImagemProperty, value); }
        }

        public static readonly DependencyProperty NomeImagemProperty =
            DependencyProperty.Register("NomeImagem", typeof(string), typeof(ItemAnimalHover), new PropertyMetadata(null));

        public ItemAnimalHover()
        {
            this.DefaultStyleKey = typeof(ItemAnimalHover);
        }

        private Grid gridAnimalItemCollapsed { get; set; }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            gridAnimalItemCollapsed = GetTemplateChild("GridItemAnimalHover") as Grid;
            gridAnimalItemCollapsed.PointerMoved += GridAnimalItemCollapsed_PointerMoved;
            gridAnimalItemCollapsed.PointerExited += GridAnimalItemCollapsed_PointerExited;
        }

        private void GridAnimalItemCollapsed_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "Normal", false);
        }

        private void GridAnimalItemCollapsed_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "Houver", false);
        }

    }
}
