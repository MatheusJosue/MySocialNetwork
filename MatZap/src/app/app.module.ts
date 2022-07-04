import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ErrorInterceptor } from './core/interceptors/error.interceptor';
import { JwtInterceptor } from './core/interceptors/jwt.interceptor';
import { MessageService } from './domain/services/message.service';
import { PostService } from './domain/services/post.service';
import { DashboardComponent } from './presentation/pages/dashboard/dashboard.component';
import { HomePageComponent } from './presentation/pages/home-page/home-page.component';
import { NavbarComponent } from './presentation/shared/navbar/navbar.component';
import { LocalDateTimePipe } from './presentation/pipe/local-date-time.pipe';
import { LoginComponent } from './presentation/pages/home-page/login/login.component';
import { RegisterComponent } from './presentation/pages/home-page/register/register.component';
import { HomeCardComponent } from './presentation/pages/home-page/home-card/home-card.component';
import { SidebarComponent } from './presentation/pages/dashboard/sidebar/sidebar.component';
import { SideMobileComponent } from './presentation/pages/dashboard/side-mobile/side-mobile.component';
import { AddFriendComponent } from './presentation/pages/dashboard/sidebar/add-friend/add-friend.component';
import { CreatePostComponent } from './presentation/pages/dashboard/sidebar/create-post/create-post.component';
import { DashCardComponent } from './presentation/pages/dashboard/dash-card/dash-card.component';
import { ListFriendsComponent } from './presentation/pages/dashboard/sidebar/list-friends/list-friends.component';
import { ListMyPostsComponent } from './presentation/pages/dashboard/sidebar/list-my-posts/list-my-posts.component';
import { ListPostsComponent } from './presentation/pages/dashboard/sidebar/list-posts/list-posts.component';
import { MessagesSentComponent } from './presentation/pages/dashboard/sidebar/messages-sent/messages-sent.component';
import { ReceivedMessagesComponent } from './presentation/pages/dashboard/sidebar/received-messages/received-messages.component';
import { UpdatePostComponent } from './presentation/pages/dashboard/sidebar/update-post/update-post.component';
import { RequestspendentsComponent } from './presentation/pages/dashboard/sidebar/requests-pendents/requestspendents.component';
import { SendMessageComponent } from './presentation/pages/dashboard/sidebar/send-message/send-message.component';
import { ChatComponent } from './presentation/pages/dashboard/sidebar/chat/chat.component';
import { ChatListComponent } from './presentation/pages/dashboard/sidebar/chat-list/chat-list.component';
import { PerfilComponent } from './presentation/pages/dashboard/sidebar/perfil/perfil.component';

@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    HomeCardComponent,
    LoginComponent,
    RegisterComponent,
    DashboardComponent,
    SidebarComponent,
    NavbarComponent,
    SideMobileComponent,
    ListPostsComponent,
    DashCardComponent,
    CreatePostComponent,
    ListMyPostsComponent,
    UpdatePostComponent,
    SendMessageComponent,
    MessagesSentComponent,
    ReceivedMessagesComponent,
    LocalDateTimePipe,
    ChatComponent,
    AddFriendComponent,
    ListFriendsComponent,
    RequestspendentsComponent,
    ChatListComponent,
    PerfilComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    NgbModule,
  ],
  providers: [
    PostService,
    MessageService,
    LocalDateTimePipe,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },],
  bootstrap: [AppComponent]
})
export class AppModule { }
