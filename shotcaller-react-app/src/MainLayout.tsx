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
        <h3>Shot Caller</h3>
        <ul style={{ listStyle: "none", padding: 0 }}>
          <li><Link to="/drinkdashboard">Drink Dashboard</Link></li>
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
