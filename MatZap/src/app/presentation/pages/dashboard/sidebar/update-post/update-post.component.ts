import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PostService } from 'src/app/domain/services/post.service';

@Component({
  selector: 'app-update-post',
  templateUrl: './update-post.component.html',
  styleUrls: ['./update-post.component.css']
})
export class UpdatePostComponent implements OnInit {

  formUp: FormGroup;
  currentPost: number = 0;

  constructor(
    private formbuilder: FormBuilder,
    private postService: PostService,
    private route: ActivatedRoute,
    private router: Router
  )

  {
    this.route.queryParams.subscribe(params => {
      this.currentPost = params['id']
    })

    this.formUp = this.formbuilder.group({
      id: [this.currentPost],
      titulo: [null],
      descricao: [null]
    });
  }

  ngOnInit(): void {
  }

  onSubmit() {
    this.postService.updatePost(this.formUp.value).subscribe(data => {
      this.router.navigate(['dashboard/listposts'])
  })
  }

  onCancel() {
    this.router.navigate(['dashboard/listposts'])
  }
}
