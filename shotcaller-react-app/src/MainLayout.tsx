import { Link, Outlet } from "react-router-dom";

export default function MainLayout() {
  return (
    <div style={{ display: "flex", height: "100vh" }}>
      {/* Left Navigation */}
      <nav style={{
        width: "220px",
        background: "#f4f4f4",
        padding: "20px",
        borderRight: "1px solid #ddd"
      }}>
        <h3>Trackers</h3>
        <ul style={{ listStyle: "none", padding: 0 }}>
          <li><Link to="/shotcaller">Shot Caller</Link></li>
          <li><Link to="/medicinecaller">Medicine Caller</Link></li>
          <li><Link to="/mealscaller">Meals Caller</Link></li>
          <li><Link to="/screentimecaller">Screen Time Caller</Link></li>
          <li><Link to="/dataStats">Data Stats</Link></li>
          {/*<li><Link to="/reports">Reports</Link></li>*/}
        </ul>

      </nav>

      {/* Right Side Routed Content */}
      <div style={{ flex: 1, padding: "20px" }}>
        <Outlet />
      </div>

    </div>
  );
}
