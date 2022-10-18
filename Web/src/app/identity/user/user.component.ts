import { Component, OnInit } from "@angular/core";
import { User } from "src/models";
import { UserService } from "src/services";

@Component({ templateUrl: "./user.component.html" })
export class UserComponent implements OnInit {
  public originalModelList = Array<User>();
  public userModelList = Array<User>();

  constructor(private userService: UserService) {}

  async ngOnInit(): Promise<void> {
    this.userModelList = await this.userService.get();
    this.originalModelList = this.userModelList.map(obj => ({ ...obj }));
  }

  add() {
    this.userModelList.splice(0, 0, { email: this.userModelList.length.toString() } as User);
  }

  async save() {
    for (let model of this.userModelList) {
      if (model.id == null) {
        await this.userService.post(model);
      } else {
        let originalModel = this.originalModelList.filter(n => n.id == model.id)[0];
        if (!this.isEqual(model, originalModel)) {
          await this.userService.put(model);
        }
      }
    }
  }

  async delete(index: any) {
    let model = this.userModelList.splice(index, 1)[0];
    if (model.id != null) {
      await this.userService.delete(model.id);
    }
  }

  isEqual(a: any, b: any) {
    // Create arrays of property names
    var aProps = Object.getOwnPropertyNames(a);
    var bProps = Object.getOwnPropertyNames(b);

    // If number of properties is different,
    // objects are not equivalent
    if (aProps.length != bProps.length) {
      return false;
    }

    for (var i = 0; i < aProps.length; i++) {
      var propName = aProps[i];

      // If values of same property are not equal,
      // objects are not equivalent
      if (a[propName] !== b[propName]) {
        return false;
      }
    }

    // If we made it this far, objects
    // are considered equivalent
    return true;
  }
}
