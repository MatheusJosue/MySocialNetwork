import { ChatListComponent } from './presentation/pages/dashboard/sidebar/chat-list/chat-list.component';
import { SendMessageComponent } from './presentation/pages/dashboard/sidebar/send-message/send-message.component';
import { UpdatePostComponent } from './presentation/pages/dashboard/sidebar/update-post/update-post.component';
import { RegisterComponent } from './presentation/pages/home-page/register/register.component';
import { MessagesSentComponent } from './presentation/pages/dashboard/sidebar/messages-sent/messages-sent.component';
import { LoginComponent } from './presentation/pages/home-page/login/login.component';
import { ListPostsComponent } from './presentation/pages/dashboard/sidebar/list-posts/list-posts.component';
import { ListMyPostsComponent } from './presentation/pages/dashboard/sidebar/list-my-posts/list-my-posts.component';
import { DashCardComponent } from './presentation/pages/dashboard/dash-card/dash-card.component';
import { CreatePostComponent } from './presentation/pages/dashboard/sidebar/create-post/create-post.component';
import { ReceivedMessagesComponent } from './presentation/pages/dashboard/sidebar/received-messages/received-messages.component';
import { ChatComponent } from './presentation/pages/dashboard/sidebar/chat/chat.component';
import { HomeCardComponent } from './presentation/pages/home-page/home-card/home-card.component';
import { ListFriendsComponent } from './presentation/pages/dashboard/sidebar/list-friends/list-friends.component';
import { RequestspendentsComponent } from './presentation/pages/dashboard/sidebar/requests-pendents/requestspendents.component';
import { AddFriendComponent } from './presentation/pages/dashboard/sidebar/add-friend/add-friend.component';

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthGuard } from './core/security/auth.guard';
import { DashboardComponent } from './presentation/pages/dashboard/dashboard.component';
import { HomePageComponent } from './presentation/pages/home-page/home-page.component';
import { PerfilComponent } from './presentation/pages/dashboard/sidebar/perfil/perfil.component';


const routes: Routes = [
  { path: '', component: HomePageComponent,
  children: [{path: '', component: HomeCardComponent},
            {path: 'home', component: HomeCardComponent},
            {path: 'login', component: LoginComponent},
            {path: 'register', component: RegisterComponent},
          ]},
  { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard],
  children: [{path: '', component: DashCardComponent},
            {path: 'profile', component: PerfilComponent},
            {path: 'createpost', component: CreatePostComponent},
            {path: 'listposts', component: ListPostsComponent},
            {path: 'updatepost', component: UpdatePostComponent},
            {path: 'listmyposts', component: ListMyPostsComponent},
            {path: 'addfriend', component: AddFriendComponent},
            {path: 'requestpendents', component: RequestspendentsComponent},
            {path: 'sendmessage', component: SendMessageComponent},
            {path: 'messagessent', component: MessagesSentComponent},
            {path: 'listfriends', component: ListFriendsComponent},
            {path: 'receivedmessages', component: ReceivedMessagesComponent},
            {path: 'chatlist', component: ChatListComponent},
            {path: 'chat', component: ChatComponent},
          ]},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
