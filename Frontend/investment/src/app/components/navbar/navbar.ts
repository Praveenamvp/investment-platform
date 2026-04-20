import { Component, OnInit, HostListener } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Router, NavigationEnd } from '@angular/router';
import { filter } from 'rxjs/operators';
import { AuthService } from '../../core/services/auth.service';

interface NavLink {
  label: string;
  route: string;
  icon: string;
}

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css',
})
export class NavbarComponent implements OnInit {
  userName = '';
  userInitials = '';
  isScrolled = false;
  isMobileOpen = false;
  isProfileOpen = false;

  navLinks: NavLink[] = [
    { label: 'Dashboard', route: '/dashboard', icon: 'grid' },
    { label: 'Stocks', route: '/stocks', icon: 'trending' },
    { label: 'Mutual Funds', route: '/mutual-funds', icon: 'pie' },
    { label: 'Portfolio', route: '/portfolio', icon: 'briefcase' },
  ];

  constructor(
    public auth: AuthService,
    private router: Router,
  ) {}

  ngOnInit(): void {
    this.userName = this.auth.getUserName();
    this.userInitials = this.getInitials(this.userName);

    // Close mobile menu on navigation
    this.router.events.pipe(filter((e) => e instanceof NavigationEnd)).subscribe(() => {
      this.isMobileOpen = false;
      this.isProfileOpen = false;
    });
  }

  @HostListener('window:scroll')
  onScroll(): void {
    this.isScrolled = window.scrollY > 10;
  }

  @HostListener('document:click', ['$event'])
  onDocumentClick(event: Event): void {
    const target = event.target as HTMLElement;
    if (!target.closest('.profile-menu')) {
      this.isProfileOpen = false;
    }
  }

  logout(): void {
    this.auth.logout();
  }

  private getInitials(name: string): string {
    return (
      name
        .split(' ')
        .map((n) => n[0])
        .slice(0, 2)
        .join('')
        .toUpperCase() || 'IQ'
    );
  }
}
