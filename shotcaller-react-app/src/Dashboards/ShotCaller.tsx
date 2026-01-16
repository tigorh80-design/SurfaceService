import { useState } from "react";
import { DataGrid } from "@mui/x-data-grid";

export default function ShotCaller() {
  const [tequilaValue, setTequilaValue] = useState(0);
  const [vodkaValue, setVodkaValue] = useState(0);
  const [beersValue, setBeersValue] = useState(0);
  const [name, setName] = useState("");
  const [status, setStatus] = useState(""); 
  const [error, setError] = useState("");

  let tequilaHue; 
  let vodkaHue; 
  let beersHue; 
  
const columns = [
  { field: "id", headerName: "ID", width: 70 },
  { field: "name", headerName: "Name", width: 150 },
  { field: "tequila", headerName: "Tequila", width: 150 },
  { field: "vodka", headerName: "Vodka", width: 150 },
  { field: "beers", headerName: "Beers", width: 150 }
];

const rows = [
  { id: 1, name: "Igor" },
  { id: 2, name: "John" },
];


  if (tequilaValue <= 5) 
  { 
    tequilaHue = 120 - tequilaValue * 12; // green → yellow } 
  }
  else { 
    tequilaHue = 60 - (tequilaValue - 5) * 12; // yellow → red }
  }

  if (vodkaValue <= 5) 
  { 
    vodkaHue = 120 - vodkaValue * 12; // green → yellow } 
  }
  else { 
    vodkaHue = 60 - (vodkaValue - 5) * 12; // yellow → red }
  }

  if (beersValue <= 5) 
  { 
    beersHue = 120 - beersValue * 12; // green → yellow } 
  }
  else { 
    beersHue = 60 - (beersValue - 5) * 12; // yellow → red }
  }

  async function submitData() 
  { 
    setStatus(""); 
    setError("");
    
      try {
            const response = await fetch("https://localhost:44375/ShotCallerController/createShotCallerRecord", 
                { method: "POST", 
                  headers: { "Content-Type": "application/json" }, 
                  body: JSON.stringify({ 
                    tequila: tequilaValue, 
                    vodka: vodkaValue, 
                    beers: beersValue,
                    name: name 
                  }) 
          });

          if (!response.ok) 
          { 
            throw new Error("API returned an error");
          }

        setStatus("Record saved successfully!");
    }
    catch (err)
    {
      setError("Failed to save record.");
    }
  }

  return (
  <>
  <h1>Shot Caller Dashboard</h1>
  <div style={{ display: "flex", gap: "40px" }}>
  {/* LEFT COLUMN — Sliders */}
  <div style={{ width: "30%" }}>
    <h2>Input Your Drink Choices</h2>
      {/* Tequila slider */}
      <div style={{ marginTop: "20px" }}>
        <label style={{ display: "block", marginBottom: "10px" }}>
          Tequila shots: {tequilaValue}
        </label>

        <input
          type="range"
          min={0}
          max={10}
          value={tequilaValue}
          onChange={(e) => setTequilaValue(Number(e.target.value))}
          style={{ width: "300px" }}
        />
      </div>
      <div style={{
            marginTop: "20px",
            width: "300px",
            height: "40px",
            backgroundColor: `hsl(${tequilaHue}, 90%, 50%)`,
            borderRadius: "6px",
            transition: "background-color 0.2s ease"
          }} 
      />

      {/* Vodka slider */}
      <div style={{ marginTop: "20px" }}>
        <label style={{ display: "block", marginBottom: "10px" }}>
          Vodka shots: {vodkaValue}
        </label>

        <input
          type="range"
          min={0}
          max={10}
          value={vodkaValue}
          onChange={(e) => setVodkaValue(Number(e.target.value))}
          style={{ width: "300px" }}
        />
        <div style={{
            marginTop: "20px",
            width: "300px",
            height: "40px",
            backgroundColor: `hsl(${vodkaHue}, 90%, 50%)`,
            borderRadius: "6px",
            transition: "background-color 0.2s ease"
          }} 
        />
      </div>

      {/* Beers slider */}
      <div style={{ marginTop: "20px" }}>
        <label style={{ display: "block", marginBottom: "10px" }}>
          Beers: {beersValue}
        </label>

        <input
          type="range"
          min={0}
          max={10}
          value={beersValue}
          onChange={(e) => setBeersValue(Number(e.target.value))}
          style={{ width: "300px" }}
        />
        <div style={{
            marginTop: "20px",
            width: "300px",
            height: "40px",
            backgroundColor: `hsl(${beersHue}, 90%, 50%)`,
            borderRadius: "6px",
            transition: "background-color 0.2s ease"
          }} 
        />
      </div>
  
      <br />
      <div>
        Name <input type="text" value={name} onChange={(e) => setName(e.target.value)} />
        <br /><br />
        <input type="button" value="Submit Data" onClick={submitData}/>
        {status && <div style={{ color: "green" }}>{status}</div>} 
        {error && <div style={{ color: "red" }}>{error}</div>}
      </div>
    </div>
    {/* RIGHT COLUMN — DataGrid */}
    <div style={{ width: "70%" }}>
      <h2>Submitted Data</h2>

      {/* Your DataGrid goes here */}
      <DataGrid rows={rows} columns={columns} />
    </div>
    </div>
    </>
  );
}
