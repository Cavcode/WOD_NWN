using WOD.Game.Server.Feature.GuiDefinition.ViewModel;
using WOD.Game.Server.Service.GuiService;

namespace WOD.Game.Server.Feature.GuiDefinition
{
    public class ModalDefinition: IGuiWindowDefinition
    {
        private readonly GuiWindowBuilder<ModalViewModel> _builder = new();

        public GuiConstructedWindow BuildWindow()
        {
            _builder.CreateWindow(GuiWindowType.Modal)
                .BindOnOpened(model => model.OnWindowOpen())
                .BindOnClosed(model => model.OnWindowClose())
                .SetIsResizable(false)
                .SetIsClosable(false)
                .SetIsCollapsed(false)
                .SetTitle(null)

                .AddColumn(col =>
                {
                    col.AddRow(row =>
                    {
                        row.AddSpacer();
                    });

                    col.AddRow(row =>
                    {
                        row.AddSpacer();
                        row.AddText()
                            .BindText(modal => modal.PromptText)
                            .SetHeight(200f);

                        row.AddSpacer();
                    });

                    col.AddRow(row =>
                    {
                        row.AddSpacer();
                        row.AddButton()
                            .BindText(model => model.ConfirmButtonText)
                            .BindOnClicked(model => model.OnConfirmClick());

                        row.AddButton()
                            .BindText(model => model.CancelButtonText)
                            .BindOnClicked(model => model.OnCancelClick());
                        row.AddSpacer();
                    });

                    col.AddRow(row =>
                    {
                        row.AddSpacer();
                    });
                });

            return _builder.Build();
        }
    }
}
