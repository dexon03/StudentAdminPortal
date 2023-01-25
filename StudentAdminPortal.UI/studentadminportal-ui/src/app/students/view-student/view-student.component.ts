import { GenderService } from './../../services/gender.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
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

  isNewStudent = true;
  header = '';
  displayProfileImageUrl = '';

  genderList: Gender[] =[];

  constructor( private readonly studentService : StudentService,
               private readonly route : ActivatedRoute,
               private readonly genderService : GenderService,
               private _snackBar: MatSnackBar,
               private router: Router) {   }

  ngOnInit() : void{
    this.route.paramMap.subscribe(
      (params) =>{
        this.studentId = params.get('id');

        if(this.studentId){

          if(this.studentId.toLowerCase() === 'Add'.toLowerCase()){
            this.isNewStudent = true;
            this.header = 'Add new student';
            this.setImage();
          }else {
            this.isNewStudent = false;
            this.header = 'Edit student';

            this.studentService.getStudent(this.studentId).subscribe(
              (successResponse) => {
                this.student = successResponse;
                this.setImage();
              },
              (errorResponse) => {
                this.setImage();
              }
            );
          }


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
        this._snackBar.open("Student updated successfully", undefined ,{
          duration: 2000
        });
      },
      (errorResponse) => {
        // Log it
      }
    )
  }

  onDelete() : void{
    this.studentService.deleteStudent(this.student.id).subscribe(
      (successResponse) => {
        this._snackBar.open("Student deleted successfully", undefined,{
          duration: 2000
        });
        setTimeout(() => {
          this.router.navigateByUrl('students');
        }, 2000)
      }
    )
  }

  onAdd() : void{
    this.studentService.createStudent(this.student)
    .subscribe(
      (successResponse) => {
        this._snackBar.open('Student added successfully', undefined, {
          duration: 2000
        });

        setTimeout(() => {
          this.router.navigateByUrl(`students/${successResponse.id}`);
        }, 2000);

      },
      (errorResponse) => {
        // Log
        console.log(errorResponse);
      }
    );
  }

  uploadImage(event: any) : void{
    if(this.studentId){
      const file : File = event.target.files[0];
      this.studentService.uploadImage(this.studentId, file).subscribe(
        (successResponse) => {
          this.student.profileImageUrl = successResponse;
          this.setImage();
          this._snackBar.open('Profile image has been updated successfully', undefined, {
            duration: 2000
          });

        },
        (errorResponse) => {

        }
      )
    }
  }

  private setImage() : void {
    if(this.student.profileImageUrl){
      // Fetch the image by url
      this.displayProfileImageUrl = this.studentService.getImagePath(this.student.profileImageUrl);
    }else{
      // Display the default image
      this.displayProfileImageUrl = '/assets/user.png';
    }
  }


}
