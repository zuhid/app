import { ComponentFixture, TestBed } from "@angular/core/testing";
import { ControlsModule } from "src/controls/controls.module";

import { Page1Component } from "./page1.component";

describe("Page1Component", () => {
  let component: Page1Component;
  let fixture: ComponentFixture<Page1Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [Page1Component],
      imports: [ControlsModule],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(Page1Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it("should create", () => {
    expect(component).toBeTruthy();
  });
});
