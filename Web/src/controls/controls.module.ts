import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { PipesModule } from "src/pipes/pipes.module";

// controls
import { CardComponent } from "./card/card.component";
import { TextComponent } from "./text/text.component";
import { CheckboxComponent } from "./checkbox/checkbox.component";
import { ButtonComponent } from "./button/button.component";
import { SvgComponent } from "./svg/svg.component";
import { TableComponent } from "./table/table.component";
import { TableColComponent } from "./table-col/table-col.component";
import { SelectComponent } from "./select/select.component";

@NgModule({
  declarations: [CardComponent, TextComponent, CheckboxComponent, ButtonComponent, SvgComponent, TableComponent, TableColComponent, SelectComponent],
  imports: [CommonModule, FormsModule, CommonModule, PipesModule],
  exports: [CardComponent, TextComponent, CheckboxComponent, ButtonComponent, SelectComponent, TableComponent, TableColComponent],
})
export class ControlsModule {}
