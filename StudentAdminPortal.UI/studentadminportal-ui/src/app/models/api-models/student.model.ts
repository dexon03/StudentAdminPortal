import { Address } from "./address.model";
import { Gender } from "./gender.model";


export interface Student{
  id:String,
  firstName:String,
  lastName:String,
  dateOfBirth: String,
  email : String,
  phoneNumber : Number,
  profileImageUrl : String,
  genderId : String,
  gender : Gender,
  address : Address,
}
