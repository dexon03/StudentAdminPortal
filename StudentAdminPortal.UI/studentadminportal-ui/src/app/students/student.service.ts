import { Student } from './../models/api-models/student.model';
import { UpdateStudentRequest } from './../models/api-models/updateStudentRequest.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { CreateStudentRequest } from '../models/api-models/createStudentRequest.model';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  private baseApiUrl = 'https://localhost:44315';
  constructor(private httpClient : HttpClient) { }

  getStudents() : Observable<Student[]> {
    return this.httpClient.get<Student[]>(this.baseApiUrl + '/students')
  }

  getStudent(studentId: string): Observable<Student>{
     return this.httpClient.get<Student>(this.baseApiUrl + '/students/' + studentId)
  }

  updateStudent(studentId: string, studentRequest : Student) : Observable<Student>{
    const updateStudentRequst : UpdateStudentRequest = {
      firstName : studentRequest.firstName,
      lastName : studentRequest.lastName,
      dateOfBirth : studentRequest.dateOfBirth,
      email : studentRequest.email,
      phoneNumber : studentRequest.phoneNumber,
      genderId : studentRequest.genderId,
      physicalAddress : studentRequest.address.physicalAddress,
      postalAddress : studentRequest.address.postalAddress
    }

    return this.httpClient.put<Student>(this.baseApiUrl + '/students/' + studentId, updateStudentRequst);
  }

  deleteStudent(studentId : string) :Observable<Student>{
    return this.httpClient.delete<Student>(this.baseApiUrl + '/students/' + studentId);
  }

  createStudent(studentRequest : Student) : Observable<Student>{
    const createStudentRequest : CreateStudentRequest = {
      firstName : studentRequest.firstName,
      lastName : studentRequest.lastName,
      dateOfBirth : studentRequest.dateOfBirth,
      email : studentRequest.email,
      phoneNumber : studentRequest.phoneNumber,
      genderId : studentRequest.genderId,
      physicalAddress : studentRequest.address.physicalAddress,
      postalAddress : studentRequest.address.postalAddress
    }

    return this.httpClient.post<Student>(this.baseApiUrl + '/students/add', createStudentRequest);
  }

  uploadImage(studentId : string, file : File) : Observable<string>{
    const formData = new FormData();
    formData.append("profileImage", file);

    return this.httpClient.post(this.baseApiUrl + '/students/' + studentId + '/upload-image', formData,
      {
        responseType : 'text'
      }
    );
  }

  getImagePath(relativeImagePath : string ){
    return this.baseApiUrl + '/' + relativeImagePath;
  }
}
