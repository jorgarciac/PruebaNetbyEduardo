import { Component } from '@angular/core';
import { MenubarModule } from 'primeng/menubar';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [MenubarModule, RouterModule],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent {
  items: any[] = [];
  ngOnInit() {
    this.items = [
      { label: 'Productos', routerLink: ['/productos'] },
      { label: 'Transacciones', routerLink: ['/transacciones'] }
    ];
  }
}
