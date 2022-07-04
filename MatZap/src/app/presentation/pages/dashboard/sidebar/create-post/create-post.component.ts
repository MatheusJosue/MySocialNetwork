import { Router } from '@angular/router';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { PostService } from 'src/app/domain/services/post.service';

@Component({
  selector: 'app-create-post',
  templateUrl: './create-post.component.html',
  styleUrls: ['./create-post.component.css']
})
export class CreatePostComponent implements OnInit {

  form: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private postService: PostService,
    private router: Router,
  )
    {
      this.form = this.formBuilder.group({
        titulo: [null],
        descricao: [null]
      });
    }

  ngOnInit(): void {
  }

  onSubmit() {
    this.postService.createPost(this.form.value).subscribe(data => {
      this.router.navigate(['dashboard/listposts'])
    })
  }

  onCancel() {
    this.router.navigate(['dashboard/listposts'])
  }
}
