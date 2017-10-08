import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'students',
    templateUrl: './students.component.html'
})
export class StudentsComponent {
    public students: Student[];

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        http.get(baseUrl + 'api/students').subscribe(result => {
            this.students = result.json() as Student[];
        }, error => console.error(error));
    }
}

interface Student {
    name: string;
    dateOfBirth: Date;
}
