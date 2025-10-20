# TaskFlow Monitor - Frontend

A modern Angular application for task management integrated with a .NET backend.

## 🚀 Technologies

This project is built with the following technologies:

- **Angular 20.3.0** - Latest Angular framework
- **TypeScript 5.9.2** - Static typing and modern JavaScript features
- **Standalone Components** - Modern architecture without NgModules
- **Reactive Forms** - Form handling with validation
- **RxJS 7.8** - Reactive programming for asynchronous operations
- **SCSS** - Advanced styling with nested rules and variables
- **Angular Router** - Navigation and routing management
- **HttpClient** - Type-safe HTTP communication with backend

## 📦 Installation and Setup

### Prerequisites

- **Node.js 18+** installed on your system
- **npm** (comes with Node.js)
- **.NET Backend** running on `http://localhost:5236`

### Installation Steps

1. Clone the repository (if not already done)

2. Install dependencies:
```bash
npm install
```

3. Start the development server:
```bash
npm start
```

The application will be available at `http://localhost:4200`

### Available Scripts

- `npm start` - Starts development server with proxy configuration
- `npm run build` - Creates production build
- `npm run watch` - Builds in watch mode for development
- `npm test` - Runs unit tests with Karma

## 🏗️ Project Structure

```
src/app/
├── components/
│   ├── task-list/          # Task list with actions (view, edit, delete)
│   └── task-form/          # Create and edit task form
├── models/
│   └── task.dto.ts         # Task data interface
├── services/
│   └── task.service.ts     # HTTP service for API communication
├── app.config.ts           # Application configuration
├── app.routes.ts           # Route definitions
└── app.*                   # Root component
```

## 🎯 How It Works

### Application Architecture

The application follows Angular's standalone component architecture, eliminating the need for NgModules. This results in a more modular and maintainable codebase.

**Key Components:**

1. **TaskListComponent** - Displays all tasks in a table format
   - Shows task details (title, description, status)
   - Provides action buttons (view, edit, delete)
   - Handles loading states and error messages
   - Confirms deletion with user before proceeding

2. **TaskFormComponent** - Handles task creation and editing
   - Uses Angular Reactive Forms with validation
   - Required field validation for title
   - Supports both create and update operations
   - Navigates back to list after successful operation

3. **TaskService** - Manages all HTTP communication
   - Provides CRUD operations
   - Returns typed Observable streams
   - Handles API endpoints communication

### Backend Communication

The application communicates with a .NET backend API through a proxy configuration.

**Proxy Setup:**

The `proxy.conf.json` file redirects all `/api` requests to the backend server running on `http://localhost:5236`. This solves CORS issues during development.

```json
{
  "/api": {
    "target": "http://localhost:5236",
    "secure": false,
    "changeOrigin": true,
    "logLevel": "debug"
  }
}
```

**API Endpoints:**

The frontend consumes the following REST endpoints:

- `GET /api/tasks` - Retrieves all tasks
- `GET /api/tasks/:id` - Retrieves a single task by ID
- `POST /api/tasks` - Creates a new task
- `PUT /api/tasks/:id` - Updates an existing task
- `DELETE /api/tasks/:id` - Deletes a task

**Data Flow:**

1. User interacts with the UI (clicks button, submits form)
2. Component calls a method from TaskService
3. TaskService makes HTTP request to backend via proxy
4. Backend processes request and returns response
5. Observable stream delivers data back to component
6. Component updates UI with new data or displays messages

### Data Model

```typescript
interface TaskDto {
  id: number;
  title: string;
  description?: string | null;
  isCompleted: boolean;
}
```

All communication between frontend and backend uses this strongly-typed interface, ensuring type safety throughout the application.

## ✨ Features

- ✅ **List Tasks** - View all tasks in a responsive table
- ✅ **Create Task** - Add new tasks with validation
- ✅ **Edit Task** - Update existing task information
- ✅ **Delete Task** - Remove tasks with confirmation dialog
- ✅ **Toggle Status** - Mark tasks as completed or pending
- ✅ **Loading States** - Visual feedback during operations
- ✅ **Error Handling** - User-friendly error messages
- ✅ **Success Messages** - Confirmation of successful operations
- ✅ **Responsive Design** - Works on desktop and mobile devices

## 🎨 Styling

The project uses a modern color palette:

- **Primary**: Purple gradient (#667eea → #764ba2)
- **Success**: Green (#28a745)
- **Danger**: Red (#dc3545)
- **Background**: Light gray (#f5f7fa)
- **Text**: Dark gray for readability

## 🔧 Configuration

### Angular Configuration

- **Standalone Components**: No NgModules required
- **Lazy Loading**: Components loaded on demand
- **Reactive Forms**: FormBuilder with validators
- **HttpClient**: Typed HTTP requests
- **Router**: Dynamic route parameters

### Development Server

The development server runs on port 4200 by default and uses the proxy configuration to communicate with the backend on port 5236.

## 📝 Code Formatting

The project uses Prettier for code formatting with the following configuration:

- Print width: 100 characters
- Single quotes
- Angular parser for HTML files

## 📄 License

This project was developed for educational purposes.
