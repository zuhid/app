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
import { MultiselectComponent } from "./multiselect/multiselect.component";
import { TypeaheadModule } from "ngx-bootstrap/typeahead";
import { ToastComponent } from "./toast/toast.component";

@NgModule({
  declarations: [
    ButtonComponent,
    CardComponent,
    CheckboxComponent,
    MultiselectComponent,
    SelectComponent,
    SvgComponent,
    TableColComponent,
    TableComponent,
    TextComponent,
    ToastComponent,
  ],
  imports: [CommonModule, FormsModule, PipesModule, TypeaheadModule.forRoot()],
  exports: [
    ButtonComponent,
    CardComponent,
    CheckboxComponent,
    MultiselectComponent,
    SelectComponent,
    TableColComponent,
    TableComponent,
    TextComponent,
    ToastComponent,
  ],
})
export class ControlsModule {}
