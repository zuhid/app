import { ArrayUtil } from "./array.util";

describe("ArrayUtil", () => {
  it("swap array items", () => {
    let arr = [1, 2, 3, 4, 5];
    ArrayUtil.swap(arr, 1, 3);
    expect(arr).toEqual([1, 4, 3, 2, 5]);
  });
  it("move array index", () => {
    let arr = ["zero", "one", "two", "three", "four"];
    ArrayUtil.move(arr, 3, 1);
    expect(arr).toEqual(["zero", "three", "one", "two", "four"]);
  });
  it("move array items", () => {
    let arr = ["zero", "one", "two", "three", "four"];
    ArrayUtil.moveItem(arr, "three", 1);
    expect(arr).toEqual(["zero", "three", "one", "two", "four"]);
  });
});
