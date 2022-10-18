export class TileUtil {
  public cols = 0;

  constructor(public tileWidth = 400, public maxCol = 4) {
    this.resize();
  }

  resize() {
    this.cols = Math.min(Math.floor(window.innerWidth / this.tileWidth), this.maxCol);
  }
}
