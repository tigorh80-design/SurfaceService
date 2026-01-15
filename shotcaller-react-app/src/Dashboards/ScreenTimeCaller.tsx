import { useState } from "react";

export default function ScreenTimeCaller() {
  const [tequilaValue, setTequilaValue] = useState(0);
  const [vodkaValue, setVodkaValue] = useState(0);
  const [beersValue, setBeersValue] = useState(0);

  let tequilaHue; 
  let vodkaHue; 
  let beersHue; 
  
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

  return (
    <div>
      <h1>Screen Time Caller Dashboard</h1>
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

    </div>
  );
}
