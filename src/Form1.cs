using System.Windows.Forms;

namespace TurnEdit;

public class SettingsJSONSyntax {
	public string FontFamily { get; set; } = "Segoe UI";
	public int FontSize { get; set; } = 11;
	public bool SaveBackupWhenFileSave { get; set; } =  false;
}

public partial class Form1 : Form
{
    public System.Windows.Forms.TextBox? maintextbox;
    public new MenuStrip? MainMenuStrip;
    public int currentformheight;
    private string? currentfilename;
      public string? SettingsSavedDirectory;
    private SettingsJSONSyntax? TurnEditSettings;
    private string? SettingsFilePath;
	private TurnEditTextCounter? textCounterForm;
	private StatusStrip? mainstatus;
	private ToolStripStatusLabel? StatusColumnAndLine;
    public Form1()
    {
        InitializeComponent();
        TurnEditPrepare();
        CreateDefaultSettings();
        LoadTurnEditSettings();
    }
    public void TurnEditPrepare()
    {
		this.textCounterForm = new TurnEditTextCounter(this);
        this.Text = "新しいファイル - TurnEdit";
        this.Size = new Size(1000, 700);
        this.currentformheight = this.Size.Height;
        TurnEditGUI();
		// this.textCounterForm = new TurnEditTextCounter(this);
        }
    public void TurnEditGUI()
    {
        this.MainMenuStrip = new MenuStrip();
        this.MainMenuStrip.Size = new Size(800, 25);
        this.MainMenuStrip.Dock = DockStyle.Top;
        this.maintextbox = new System.Windows.Forms.TextBox();
        this.maintextbox.Dock = DockStyle.Fill;
        this.maintextbox.Multiline = true;
		this.mainstatus = new StatusStrip();
		this.mainstatus.Dock = DockStyle.Bottom;
		this.StatusColumnAndLine = new ToolStripStatusLabel();
		this.StatusColumnAndLine.Text = "行: 列: ";
		this.mainstatus.Items.Add(this.StatusColumnAndLine);
        /* "File" Menu */
        ToolStripMenuItem FileMenuItem = new ToolStripMenuItem();
        FileMenuItem.Name = "FileMenuItem";
        FileMenuItem.Text = "ファイル";
        ToolStripMenuItem FileNewMenuItem = new ToolStripMenuItem();
        FileNewMenuItem.Name = "FileNewMenuItem";
        FileNewMenuItem.Text = "新規作成";
        FileNewMenuItem.Click += new EventHandler(this.FileNewMenuItem_Click);
        ToolStripMenuItem FileOpenMenuItem = new ToolStripMenuItem();
        FileOpenMenuItem.Name = "FileOpenMenuItem";
        FileOpenMenuItem.Text = "開く";
        FileOpenMenuItem.Click += new EventHandler(this.FileOpenMenuItem_Clicked);
        ToolStripMenuItem FileSaveMenuItem = new ToolStripMenuItem();
        FileSaveMenuItem.Name = "FileSaveMenuItem";
        FileSaveMenuItem.Text = "上書き保存";
		FileSaveMenuItem.Click += new EventHandler(this.FileSaveMenuItem_Click);
        ToolStripMenuItem FileSaveAsMenuItem = new ToolStripMenuItem();
        FileSaveAsMenuItem.Name = "FileSaveAsMenuItem";
        FileSaveAsMenuItem.Text = "名前を付けて保存";
        FileSaveAsMenuItem.Click += new EventHandler(this.FileSaveAsMenuItem_Click);
		ToolStripMenuItem FileExitMenuItem = new ToolStripMenuItem();
		FileExitMenuItem.Name = "FileExitMenuItem";
		FileExitMenuItem.Text = "終了";
		FileExitMenuItem.Click += new EventHandler(this.FileExitMenuItem_Click);
        FileMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
            FileNewMenuItem,
            FileOpenMenuItem,
            FileSaveMenuItem,
            FileSaveAsMenuItem,
			FileExitMenuItem
        });
        /* "Edit" Menu */
        ToolStripMenuItem EditMenuItem = new ToolStripMenuItem();
        EditMenuItem.Name = "EditMenuItem";
        EditMenuItem.Text = "編集";
        ToolStripMenuItem  EditSearchMenuItem = new ToolStripMenuItem();
        EditSearchMenuItem.Name = "EditSearchMenuItem";
        EditSearchMenuItem.Text = "検索";
        EditSearchMenuItem.Click += new EventHandler(this.EditSearchMenuItem_Click);
        ToolStripMenuItem EditReplaceMenuItem = new ToolStripMenuItem();
        EditReplaceMenuItem.Name = "EditReplaceMenuItem";
        EditReplaceMenuItem.Text = "置き換え";
		ToolStripMenuItem EditNowMenuItem = new ToolStripMenuItem();
		EditNowMenuItem.Name = "EditNowMenuItem";
		EditNowMenuItem.Text = "日付と時間を挿入";
		EditNowMenuItem.Click += new EventHandler(this.EditNowMenuItem_Click);
        EditReplaceMenuItem.Click += new EventHandler(this.EditReplaceMenuItem_Click);
        EditMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
            EditSearchMenuItem,
            EditReplaceMenuItem,
			EditNowMenuItem
        });
        /* "Settings" Menu */
        ToolStripMenuItem SettingsMenuItem = new ToolStripMenuItem();
        SettingsMenuItem.Name = "SettingsMenuItem";
        SettingsMenuItem.Text = "設定";
        ToolStripMenuItem SettingsFontMenuItem = new ToolStripMenuItem();
        SettingsFontMenuItem.Name = "SettingsFontMenuItem";
        SettingsFontMenuItem.Text = "フォント";
        SettingsFontMenuItem.Click += new EventHandler(this.SettingsFontMenuItem_Click);
        SettingsMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
            SettingsFontMenuItem
        });
		/* "Tools" Menu */
		ToolStripMenuItem ToolsMenuItem = new ToolStripMenuItem();
		ToolsMenuItem.Name = "ToolsMenuItem";
		ToolsMenuItem.Text = "ツール";
		ToolStripMenuItem ToolsCounterMenuItem = new ToolStripMenuItem();
		ToolsCounterMenuItem.Name = "ToolsCounterMenuItem";
		ToolsCounterMenuItem.Text = "文字のカウント";
		ToolsCounterMenuItem.Click += new EventHandler(this.ToolsCounterMenuItem_Click);
		ToolsMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
			ToolsCounterMenuItem
		});
        /* "Help" Menu */
        ToolStripMenuItem HelpMenuItem = new ToolStripMenuItem();
        HelpMenuItem.Name = "HelpMenuItem";
        HelpMenuItem.Text = "ヘルプ";
		ToolStripMenuItem HelpOnlineMenuItem = new ToolStripMenuItem();
		HelpOnlineMenuItem.Name = "HelpOnlineMenuItem";
		HelpOnlineMenuItem.Text = "オンラインヘルプ";
		HelpOnlineMenuItem.Click += new EventHandler(this.HelpOnlineMenuItem_Click);
        ToolStripMenuItem HelpAboutMenuItem = new ToolStripMenuItem();
        HelpAboutMenuItem.Name = "HelpAboutMenuItem";
        HelpAboutMenuItem.Text = "概要";
        HelpAboutMenuItem.Click += new EventHandler(this.HelpAboutMenuItem_Clicked);
        HelpMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
            HelpOnlineMenuItem,
			HelpAboutMenuItem
        });
        this.MainMenuStrip.Items.AddRange(new ToolStripItem[] {
            FileMenuItem,
            EditMenuItem,
			ToolsMenuItem,
            SettingsMenuItem,
            HelpMenuItem
        });
		this.Controls.Add(this.maintextbox);
        this.Controls.Add(MainMenuStrip);
		this.Controls.Add(this.mainstatus);
		this.maintextbox.TextChanged += new EventHandler(this.maintextboxTextChangedEvent);
		this.maintextbox.KeyUp += new KeyEventHandler(this.maintextbox_KeyUp);
    }
	public void maintextbox_KeyUp(object? sender, KeyEventArgs e) {
		
	}
	public void UpdateColumnAndLineStatus() {
		
	}
    public void FileNewMenuItem_Click(object? sender, EventArgs e)
    {
        this.maintextbox!.Clear();
		this.currentfilename = null;
    }
    public void HelpAboutMenuItem_Clicked(object? sender, EventArgs e) {
        var AboutForm = new TurnEditAboutForm();
        AboutForm.Show();
    }
    public void FileOpenMenuItem_Clicked(object? sender, EventArgs e) {
        OpenFileDialog opendialog = new OpenFileDialog();
        opendialog.ShowHelp = true;
        opendialog.CheckFileExists = true;
        opendialog.Filter = "テキストファイル(*.txt)|*.txt|すべてのファイル (*.*)|*.*";
        if (opendialog.ShowDialog() == DialogResult.OK) {
            string OpenedFileName = opendialog.FileName;
            string OpenFileContent = File.ReadAllText(OpenedFileName);
            this.maintextbox!.Text = OpenFileContent;
            this.Text = @$"{OpenedFileName} - TurnEdit";
            this.currentfilename = OpenedFileName;
        }
    }
    public void FileSaveAsMenuItem_Click(object? sender, EventArgs e) {
        SaveFileDialog savedialog = new SaveFileDialog();
        savedialog.ShowHelp = true;
        savedialog.Filter = "テキストファイル (*.txt)|*.txt|すべてのファイル (*.*)|*.*";
        if (savedialog.ShowDialog() == DialogResult.OK) {
            string savefilename = savedialog.FileName;
            File.WriteAllText(savefilename, this.maintextbox!.Text);
            this.Text = @$"{savefilename} - TurnEdit";
            this.currentfilename = savefilename;
        }
    }
	public void FileExitMenuItem_Click(object? sender, EventArgs e) {
		Application.Exit();
	}
    public void EditSearchMenuItem_Click(object? sender, EventArgs e) {
        var SearchForm = new TurnEditSearchForm(this);
        SearchForm.Show();
    }
    public void EditReplaceMenuItem_Click(object? sender, EventArgs e) {
        var ReplaceForm = new TurnEditReplaceForm(this);
        ReplaceForm.Show();
    }
	public void EditNowMenuItem_Click(object? sender, EventArgs e) {
		DateTime now = DateTime.Now;
		string nowStr = now.ToString();
		this.maintextbox!.SelectedText = nowStr;
		this.maintextbox.SelectionStart = this.maintextbox.SelectionStart + nowStr.Length;
		this.maintextbox.ScrollToCaret();
	}
    public void SettingsFontMenuItem_Click(object? sender, EventArgs e) {
        FontDialog fontdlg = new FontDialog();
        fontdlg.Font = this.maintextbox!.Font;
        if (fontdlg.ShowDialog() == DialogResult.OK) {
            this.maintextbox.Font = fontdlg.Font;
            this.TurnEditSettings!.FontFamily = fontdlg.Font.FontFamily.Name;
            this.TurnEditSettings.FontSize = (int)fontdlg.Font.Size;
            SaveTurnEditSettings(this.TurnEditSettings);
        }
    }
    public void CreateDefaultSettings() {
    	this.TurnEditSettings = new SettingsJSONSyntax
    	{
    		FontFamily = "Segoe UI",
    		FontSize = 11,
    		SaveBackupWhenFileSave = false
    	};
    }
    public bool SaveTurnEditSettings(SettingsJSONSyntax settings) {
    	try {
    		this.SettingsFilePath = Path.Combine(Application.LocalUserAppDataPath, "turnedit-settings.json");
    		System.Text.Json.JsonSerializerOptions options = new System.Text.Json.JsonSerializerOptions { WriteIndented = true };
    		string JSONString = System.Text.Json.JsonSerializer.Serialize(settings, options);
    		File.WriteAllText(this.SettingsFilePath, JSONString);
    		return true;
    	} catch (Exception ex) {
    		MessageBox.Show($@"設定を保存できませんでした: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
    		return false;
    	}
    }
    public bool LoadTurnEditSettings() {
    	try {
    	var SettingsJSONPathForLoad = Path.Combine(Application.LocalUserAppDataPath, "turnedit-settings.json");
    	var SettingsJSONContent = File.ReadAllText(SettingsJSONPathForLoad);
    	SettingsJSONSyntax? DeserializedJSON = System.Text.Json.JsonSerializer.Deserialize<SettingsJSONSyntax>(SettingsJSONContent);
    	if (DeserializedJSON is not null) {
			this.maintextbox!.Font = new Font(DeserializedJSON.FontFamily, DeserializedJSON.FontSize);
			this.TurnEditSettings = DeserializedJSON;
		}
		return true;
    	} catch (Exception ex) {
    	MessageBox.Show($@"設定の読み込みに失敗しました: {ex.Message}。 デフォルト設定を使用します。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
    	CreateDefaultSettings();
    	this.maintextbox!.Font = new Font(this.TurnEditSettings!.FontFamily, this.TurnEditSettings!.FontSize);
    	return false;
    	}
    }
	public void ToolsCounterMenuItem_Click(object? sender, EventArgs e) {
		var CounterInstanceForShowDlg = new TurnEditTextCounter(this);
		CounterInstanceForShowDlg.Show();
	}
	public void maintextboxTextChangedEvent(object? sender, EventArgs e) {
		
		this.textCounterForm!.UpdateCounter();
	}
	public void FileSaveMenuItem_Click(object? sender, EventArgs e) {
		if (this.currentfilename != null) {
			try {
				File.WriteAllText(this.currentfilename!, this.maintextbox!.Text);
			} catch (Exception ex) {
				MessageBox.Show($@"ファイルを保存できませんでした: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		} else {
			MessageBox.Show("ファイルを開いてください。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}
	}
	public void HelpOnlineMenuItem_Click(object? sender, EventArgs e) {
		try {
			System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo{
			FileName = "https://github.com/suzuki3932/TurnEdit/wiki",
			UseShellExecute = true
			});
		} catch (Exception ex) {
			MessageBox.Show($@"オンラインヘルプを開けませんでした: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}
}
