import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';

import reportWebVitals from './reportWebVitals';
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import Menu from './Components/NavBar/Menu';
import Home from './Components/BackGround/Home';
import BrewingForm from './Components/PotionBrewingForm/BrewingForm';
import StudentList from './Components/StudentList/StudentList';

const router = createBrowserRouter([
  {
    path: "/",
    element: <Menu />,
    children: [
      {
        path: "/",
        element: <Home/>,
      },
      {
        path: "/brewing-form",
        element: <BrewingForm />
      },
      {
        path: "/students",
        element: <StudentList/>
      }
    ]
  }
])

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <RouterProvider router = {router}/>
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
