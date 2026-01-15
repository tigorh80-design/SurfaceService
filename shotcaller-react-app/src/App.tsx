import { Routes, Route } from "react-router-dom";
import MainLayout from "./MainLayout";
import ShotCaller from "./Dashboards/ShotCaller";
import MedicineCaller from "./Dashboards/MedicineCaller";
import MealsCaller from "./Dashboards/MealsCaller";
import ScreenTimeCaller from "./Dashboards/ScreenTimeCaller";
import DataStats from "./DataStats";

function App() {

  return (
  <Routes> 
    {/* Parent route uses MainLayout */} 
    <Route path="/" element={<MainLayout />}>
      {/* Child routes render inside <Outlet /> */} 
      <Route path="shotcaller" element={<ShotCaller />} />
      <Route path="medicinecaller" element={<MedicineCaller />} />
      <Route path="mealscaller" element={<MealsCaller />} />
      <Route path="screentimecaller" element={<ScreenTimeCaller />} />
      <Route path="dataStats" element={<DataStats />} />
      {/*<Route path="reports" element={<Reports />} />*/}
    </Route>
  </Routes>
  )
}

export default App
