using static System.Net.Mime.MediaTypeNames;

namespace Algo4;

public partial class MainPage : ContentPage
{
    int delay = 600;
    int algo = 0;
    String stroka;
    int id = -1;

    private StrokaViewModel viewModel;

    public MainPage()
    {
        InitializeComponent();
        viewModel = new StrokaViewModel("");
        BindingContext = viewModel;
    }

    async public Task<int> BM(String slovo, String stroka)
    {
        if (string.IsNullOrEmpty(slovo) || string.IsNullOrEmpty(stroka)) return -1;

        Dictionary<Char, int> Table = new Dictionary<char, int>();

        int dlinaSlova = slovo.Length;
        int dlinaStroki = stroka.Length;

        for (int i = 0; i < dlinaSlova - 1; i++)
        {
            Table[slovo[i]] = dlinaSlova - 1 - i;
        }


        int skip = 0;
        viewModel.windowStart = 0;
        viewModel.setWindoW(dlinaSlova);
        viewModel.updateComparison(skip);

        while (dlinaStroki - skip >= dlinaSlova)
        {
            collection.ScrollTo(skip, -1, ScrollToPosition.Center, true);
            await Task.Delay(delay);
            int i = dlinaSlova - 1;
            while (stroka[skip + i] == slovo[i])
            {
                viewModel.CharColorItems[skip + i].TextColor = Colors.Green;
                await Task.Delay(delay);
                if (i == 0) return skip;
                i--;
            }
            viewModel.CharColorItems[skip + i].TextColor = Colors.Red;
            char badChar = stroka[skip + dlinaSlova - 1];
            if (Table.ContainsKey(badChar)) skip += Table[badChar];
            else skip += dlinaSlova;
            viewModel.shiftWindow(skip, dlinaSlova);
        }
        return -1;
    }

    public async Task<int> KMP(String slovo, String stroka)
    {
        int[] massiv = new int[slovo.Length];
        int prevLPS = 0, i = 1;

        while (i < slovo.Length)
        {
            if (slovo[prevLPS] == slovo[i])
            {
                prevLPS++;
                massiv[i] = prevLPS;
                i++;
            }
            else if (prevLPS == 0)
            {
                massiv[i] = 0;
                i++;
            }
            else prevLPS = massiv[prevLPS - 1];
        }

        int strokaID = 0, slovoID = 0;

        int windowStart = 0;
        viewModel.windowStart = windowStart;
        viewModel.setWindoW(slovo.Length);
        viewModel.updateComparison(strokaID);

        while (strokaID < stroka.Length)
        {
            collection.ScrollTo(windowStart, -1, ScrollToPosition.Center, true);
            await Task.Delay(delay);
            if (stroka[strokaID] == slovo[slovoID])
            {
                viewModel.CharColorItems[strokaID].TextColor = Colors.Green;
                strokaID++;
                slovoID++;
                viewModel.updateComparison(strokaID);
            }
            else
            {
                viewModel.CharColorItems[strokaID].TextColor = Colors.Red;
                if (slovoID == 0)
                {
                    strokaID++;
                    windowStart++;
                    viewModel.shiftWindow(windowStart, slovo.Length);
                    viewModel.updateComparison(strokaID);
                }
                else
                {
                    slovoID = massiv[slovoID - 1];
                    windowStart = strokaID - slovoID;
                    viewModel.shiftWindow(windowStart, slovo.Length);
                }
            }
            if (slovoID == slovo.Length)
            {
                return strokaID - slovo.Length;
            }
        }
        return -1;
    }


    void confirmStrokaButton_Clicked(System.Object sender, System.EventArgs e)
    {
        stroka = entryStroka.Text;
        viewModel.UpdateCollection(stroka);
        searchButton.IsEnabled = true;
    }


    async void searchButton_Clicked(System.Object sender, System.EventArgs e)
    {
        searchButton.IsEnabled = false;
        confirmStrokaButton.IsEnabled = false;

        viewModel.ResetColors();
        if (algo is 0)
        {
            id = await KMP(entrySlovo.Text, stroka);
        }
        else if (algo is 1)
        {
            id = await BM(entrySlovo.Text, stroka);
        }

        if (id == -1) labelResult.Text = "Слово не найдено";
        else
        {
            viewModel.Highlight(id, entrySlovo.Text.Length);
        }
        collection.ScrollTo(id, -1, ScrollToPosition.Center, true);
        labelResult.Text = id.ToString();

        searchButton.IsEnabled = true;
        confirmStrokaButton.IsEnabled = true;
    }

    void algoswitcher1_Clicked(System.Object sender, System.EventArgs e)
    {
        algo = 0;
        algoswitcher1.IsEnabled = false;
        algoswitcher2.IsEnabled = true;
    }

    void algoswitcher2_Clicked(System.Object sender, System.EventArgs e)
    {
        algo = 1;
        algoswitcher1.IsEnabled = true;
        algoswitcher2.IsEnabled = false;
    }
}