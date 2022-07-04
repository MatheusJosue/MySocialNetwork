import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal, NgbModalConfig } from '@ng-bootstrap/ng-bootstrap';
import { Post } from 'src/app/domain/models/post.model';
import { User } from 'src/app/domain/models/user.model';
import { AuthenticationService } from 'src/app/domain/services/authentication.service';
import { PostService } from 'src/app/domain/services/post.service';

@Component({
  selector: 'app-list-my-posts',
  templateUrl: './list-my-posts.component.html',
  styleUrls: ['./list-my-posts.component.css']
})
export class ListMyPostsComponent implements OnInit {

  deletePostId : number = 0;
  users: User[] = [];
  posts: Post[] = [];
  currentUser: User;

  constructor(
    private postService: PostService,
    private authenticationService: AuthenticationService,
    config: NgbModalConfig,
    private modalService: NgbModal,
    private router: Router
  )
  {
    this.currentUser = authenticationService.currentUserValue

    config.backdrop = 'static';
    config.keyboard = false;
  }

  ngOnInit(): void {
    this.listMyPosts();
  }

  open(content: any, id: any) {
    this.modalService.open(content);
    this.deletePostId = id;
  }

  listMyPosts(){
    this.postService.listMyPosts().subscribe((data : any) => {
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

  onCancel() {
    this.router.navigate(['dashboard/listposts'])
  }
}
