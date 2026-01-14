import { Routes, Route } from "react-router-dom";
import MainLayout from "./MainLayout";
import DrinkDashboard from "./DrinkDashboard";
import DataStats from "./DataStats";

function App() {

  return (
  <Routes> 
    {/* Parent route uses MainLayout */} 
    <Route path="/" element={<MainLayout />}>
      {/* Child routes render inside <Outlet /> */} 
      <Route path="drinkdashboard" element={<DrinkDashboard />} />
      <Route path="dataStats" element={<DataStats />} />
      {/*<Route path="reports" element={<Reports />} />*/}
    </Route>
  </Routes>
  )
}

export default App
