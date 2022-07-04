import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal, NgbModalConfig } from '@ng-bootstrap/ng-bootstrap';
import { Post } from 'src/app/domain/models/post.model';
import { User } from 'src/app/domain/models/user.model';
import { AuthenticationService } from 'src/app/domain/services/authentication.service';
import { PostService } from 'src/app/domain/services/post.service';

@Component({
  selector: 'app-list-posts',
  templateUrl: './list-posts.component.html',
  styleUrls: ['./list-posts.component.css']
})
export class ListPostsComponent implements OnInit {

  deletePostId : number = 0;
  users: User[] = [];
  posts: Post[] = [];
  currentUser: User;
  liked: Post[] = [];

  constructor(
    private postService: PostService,
    private authenticationService: AuthenticationService,
    config: NgbModalConfig,
    private modalService: NgbModal,
    private router: Router
  )
  {
    this.currentUser = authenticationService.currentUserValue
  }

  ngOnInit(): void {
    this.listPosts();
  }

  open(content: any, id: any) {
    this.modalService.open(content);
    this.deletePostId = id;
  }

  listPosts(){
    this.postService.listPosts().subscribe((data : any) => {
      this.posts = data;
    })
  }

  updatePost(id:any){
    this.router.navigate(['dashboard/updatepost'], {queryParams: {id: id}})
  }

  deletePost(){
    this.postService.deletePost(this.deletePostId).subscribe((data: any) =>{
      window.location.reload();
    })
  }

  cancel() {
    this.router.navigate(['dashboard'])
  }

  createLike(id:any){
    this.postService.createLike(id).subscribe((data: any) =>{
      this.ngOnInit();
    })
  }

  removeLike(id:any){
    this.postService.removeLike(id).subscribe((data: any) =>{
      this.ngOnInit();
    })
  }

}
