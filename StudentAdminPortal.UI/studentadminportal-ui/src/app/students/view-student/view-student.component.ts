import { GenderService } from './../../services/gender.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Student } from 'src/app/models/ui-models/student.model';
import { StudentService } from '../student.service';
import { Gender } from 'src/app/models/ui-models/gender.model';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-view-student',
  templateUrl: './view-student.component.html',
  styleUrls: ['./view-student.component.css']
})
export class ViewStudentComponent implements OnInit {
  studentId : string | null | undefined;
  student : Student = {
    id : '',
    firstName : '',
    lastName : '',
    dateOfBirth : '',
    email : '',
    phoneNumber : 0,
    genderId : '',
    profileImageUrl : '',
    gender:{
      id : '',
      description : ''
    },
    address : {
      id : '',
      physicalAddress : '',
      postalAddress : '',
    }
  };
  genderList: Gender[] =[];

  constructor( private readonly studentService : StudentService,
               private readonly route : ActivatedRoute,
               private readonly genderService : GenderService,
               private _snackBar: MatSnackBar) {   }

  ngOnInit() : void{
    this.route.paramMap.subscribe(
      (params) =>{
        this.studentId = params.get('id');

        if(this.studentId){
          this.studentService.getStudent(this.studentId).subscribe(
            (successResponse) => {
              this.student = successResponse;
            }
          );

          this.genderService.getGenderList().subscribe(
            (successResponse) => {
              this.genderList = successResponse;
            }
          )

        }
      }
    );

  }

  onUpdate(): void{
    this.studentService.updateStudent(this.student.id, this.student).subscribe(
      (successResponse) => {
        //Show a notification
        this._snackBar.open("Student updated successfully", 'test',{
          duration: 2000
        });
      },
      (errorResponse) => {
        // Log it
      }
    )


    // Call Student Service to update student
  }

}
