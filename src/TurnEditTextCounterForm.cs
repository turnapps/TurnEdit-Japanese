namespace TurnEdit;

public class TurnEditTextCounter : Form {
    private Form1 _mainForm;
    private Label TurnEditTextCounterLabel;

    public TurnEditTextCounter(Form1 mainForm) {
        _mainForm = mainForm;

        this.Text = "文字のカウント";
        this.Size = new Size(200, 100);
        this.MaximumSize = this.Size;
        this.MinimumSize = this.Size;
        this.MaximizeBox = false;
        this.MinimizeBox = false;

        this.TurnEditTextCounterLabel = new Label();
        this.TurnEditTextCounterLabel.AutoSize = true;
        this.TurnEditTextCounterLabel.Dock = DockStyle.Top;
        this.Controls.Add(this.TurnEditTextCounterLabel);
        UpdateCounter(); 
    }

    public void UpdateCounter() {
        if (_mainForm?.maintextbox != null && TurnEditTextCounterLabel != null) {
            var TextBoxTextCountReal = _mainForm.maintextbox.Text.Length;
            this.TurnEditTextCounterLabel.Text = "文字の総数(改行を含む): " + TextBoxTextCountReal;
        } else {
            if (TurnEditTextCounterLabel != null)
            {
                this.TurnEditTextCounterLabel.Text = "文字の総数: 0 (エラー)";
            }
        }
    }
}