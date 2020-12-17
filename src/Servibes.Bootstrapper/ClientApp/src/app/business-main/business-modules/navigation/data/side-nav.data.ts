import { SideNavItems, SideNavSection } from './../../navigation/models';

export const sideNavSections: SideNavSection[] = [
    {
        text: 'Business profile',
        items: ['appointments', 'sales', 'clientBase', 'reviews', 'profile'],
    }
];

export const sideNavItems: SideNavItems = {
    appointments: {
        icon: '',
        text: 'Appointments',
        link: '/business/appointments',
    },
    sales: {
        icon: '',
        text: 'Sales',
        submenu: [
            {
                text: 'Sales',
                link: '/business/sales'
            },
            {
                text: 'History',
                link: '/business/sales/history',
            }
        ],
    },
    clientBase: {
        icon: '',
        text: 'Client base',
        link: '/business/clientbase'
    },
    reviews: {
        icon: '',
        text: 'Reviews',
        link: '/business/reviews'
    },
    profile: {
        icon: '',
        text: 'Profile',
        submenu: [
            {
                text: 'Profile',
                link: '/business/profile'
            },
            {
                text: 'Employees',
                link: '/business/profile/employees'
            }, 
            {
                text: 'Services',
                link: '/business/profile/services'
            }
        ]
    }
};
