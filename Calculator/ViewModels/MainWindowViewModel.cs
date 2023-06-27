using System.Windows.Input;
using Calculator.Models;
using Prism.Commands;
using ReactiveUI;

namespace Calculator.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly ICalculator _calculator;
    private string _displayText = "0";
    public MainWindowViewModel(ICalculator calculator)
    {
        _calculator = calculator;
        SetInputSymbolCommand = new DelegateCommand<string?>(SetSymbol);
        CalcCommand = new DelegateCommand(OnCalc);
        ClearLastCommand = new DelegateCommand(OnClearLastSymbol);
        AllClearCommand = new DelegateCommand(AllClear);
        PercentCommand = new DelegateCommand(Percent);

    }

    private void Percent()
    {
        DisplayText = _calculator.Calculate(DisplayText + "/100");
    }

    private void AllClear()
    {
        DisplayText = "0";
    }

    private void OnClearLastSymbol()
    {
        if (!string.IsNullOrWhiteSpace(DisplayText))
        {
            DisplayText = DisplayText[..^1];
            if (string.IsNullOrWhiteSpace(DisplayText))
            {
                AllClear();
            }
        }
    }

    private void OnCalc()
    {
        DisplayText = _calculator.Calculate(DisplayText);
    }

    public string DisplayText
    {
        get => _displayText;
        set
        {
            _displayText = value;
            OnPropertyChanged();
        } 
    }

    public ICommand SetInputSymbolCommand { get; }
    public ICommand CalcCommand { get; }
    public ICommand ClearLastCommand { get; }
    public ICommand AllClearCommand { get; }
    
    public ICommand PercentCommand { get; }

   

    private void SetSymbol(string? parametr)
    {
        if (DisplayText == "0" && IsDigit(parametr))
        {
            DisplayText = string.Empty;
        }
        DisplayText += parametr;
       
    }

    private static bool IsDigit(string? parametr) => int.TryParse(parametr, out var d);
}