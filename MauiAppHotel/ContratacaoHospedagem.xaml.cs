namespace MauiAppHotel;

public partial class ContratacaoHospedagem : ContentPage
{
    // Propriedades
    int _qntAdultos;
    public int QntAdultos
    {
        get => _qntAdultos;
        set
        {
            if (_qntAdultos != value)
            {
                _qntAdultos = value;
                OnPropertyChanged();
            }
        }
    }

    int _qntCriancas;
    public int QntCriancas
    {
        get => _qntCriancas;
        set
        {
            if (_qntCriancas != value)
            {
                _qntCriancas = value;
                OnPropertyChanged();
            }
        }
    }

    int diarias;
    double valor_suite;
    double valor_total;

    public ContratacaoHospedagem()
    {
        InitializeComponent();
        this.BindingContext = this;
    }

    private void stpAdultos_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        QntAdultos = (int)e.NewValue;
    }

    private void stpCriancas_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        QntCriancas = (int)e.NewValue;
    }

    private void dtpckCheckin_DateSelected(object sender, DateChangedEventArgs e)
    {

        dtpckCheckout.MinimumDate = e.NewDate;

        if (dtpckCheckout.Date < e.NewDate)
        {
            dtpckCheckout.Date = e.NewDate;
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (pckQuartos.SelectedIndex == -1)
            {
                DisplayAlert("Atenção", "Selecione um tipo de quarto.", "OK");
                return;
            }

            switch (pckQuartos.SelectedIndex)
            {
                case 0: valor_suite = 110.0; break; // Suíte Master
                case 1: valor_suite = 90.0; break;  // Quarto Família
                case 2: valor_suite = 75.0; break;  // Quarto Casal
                case 3: valor_suite = 55.0; break;  // Quarto Solteiro
                default: valor_suite = 0; break;
            }

            TimeSpan ts = dtpckCheckout.Date - dtpckCheckin.Date;
            diarias = ts.Days;

            if (diarias <= 0)
            {
                DisplayAlert("Atenção", "A data de Check-out deve ser pelo menos um dia depois do Check-in.", "OK");
                return;
            }

            lblTotalEstadia.Text = $"Total de {diarias} diárias.";

            valor_total = (QntAdultos * valor_suite * diarias) + (QntCriancas * (valor_suite / 2) * diarias);

            lblValorTotal.Text = $"Valor total: R$ {valor_total:F2}";
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", $"Ocorreu um erro inesperado: {ex.Message}", "OK");
        }
    }
}