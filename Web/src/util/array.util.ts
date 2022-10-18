export class ArrayUtil {
  static swap(arr: any[], x: number, y: number) {
    [arr[x], arr[y]] = [arr[y], arr[x]];
  }

  static move(arr: any[], oldIndex: number, newIndex: number) {
    let oldItem = arr.splice(oldIndex, 1);
    if (oldItem.length == 1) {
      arr.splice(newIndex, 0, oldItem[0]);
    }
  }

  static moveItem(arr: any[], item: any, newIndex: number) {
    let oldIndex = arr.indexOf(item);
    arr.splice(oldIndex, 1);
    arr.splice(newIndex, 0, item);
  }

  static sleep(ms: number) {
    return new Promise(resolve => setTimeout(resolve, ms));
  }
}
