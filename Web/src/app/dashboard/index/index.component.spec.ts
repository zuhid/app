import { ComponentFixture, fakeAsync, TestBed, tick } from "@angular/core/testing";

import { IndexComponent } from "./index.component";

describe("IndexComponent", () => {
  let component: IndexComponent;
  let fixture: ComponentFixture<IndexComponent>;
  let dragStart = new DragEvent("dragstart", { dataTransfer: new DataTransfer() });

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [IndexComponent],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IndexComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  describe("zindex", () => {
    it("iinital value to be -1", () => {
      expect(component.zindex).toBe(-1);
    });
  });

  describe("dragstart", () => {
    it("set zindex to 1", fakeAsync(() => {
      expect(component.zindex).toBe(-1);
      component.dragstart(dragStart, "");
      tick(200);
      expect(component.zindex).toBe(1);
    }));
    // it("set previousKey", () => {
    //   expect(dragStart.dataTransfer?.getData("previousKey")).toBe("");
    //   component.dragstart(dragStart, "25");
    //   expect(dragStart.dataTransfer?.getData("previousKey")).toBe("25");
    // });
  });

  describe("dragend", () => {
    it("set zindex to -1", () => {
      component.zindex = 10;
      expect(component.zindex).toBe(10);
      component.dragend();
      expect(component.zindex).toBe(-1);
    });
  });

  describe("drop", () => {
    it("set one in third position", () => {
      component.tileOrder = ["zero", "one", "two", "three", "four"];
      dragStart.dataTransfer?.setData("previousKey", "three");
      component.drop(dragStart, 1);
      expect(component.tileOrder).toEqual(["zero", "three", "one", "two", "four"]);
    });
  });

  describe("getOrder", () => {
    it("'two' to be in the index '1'", () => {
      component.tileOrder = ["one", "two", "three"];
      expect(component.getOrder("two")).toBe(1);
    });
  });
});
