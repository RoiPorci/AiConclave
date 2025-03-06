# AiConclave

## Description
AiConclave is a strategy/board-like game that immerses players in the present, where each group controls a major world faction (European Union, USA, China, etc.). The goal is to develop the artificial intelligence of your faction while achieving nation-specific objectives.

The game is played in turns, where each faction can execute **two actions per turn**. These actions directly impact technological, economic, and diplomatic development, with long-term consequences.

Players must navigate **agreements, betrayals, exchanges**, and strategic decisions to ensure the rise of their AI while considering global repercussions.

## Technologies Used
- **Frontend**: React / TypeScript (SPA)
- **Backend**: .NET 8 (REST API, not strictly clean architecture)
- **Database**: SQL Server

## Available Actions

Players can choose from several strategic action categories:

### I. AI & Technology
- Invest in **AI research and development**
- Optimize **industrial and governmental processes** through AI
- Regulate or **slow down** technological advances for ethical or strategic reasons
- Assess the impact of **surveillance, militarization, and algorithmic biases**

### II. Energy & Climate
- Manage **energy supply** (fossil fuels, renewables, etc.)
- Implement **ecological reforms** and environmental policies
- Respond to **natural disasters** and energy crises
- Balance **economic growth and climate commitments**

### III. Economy & Trade
- Stimulate or slow down the economy through **strategic investments**
- Negotiate **trade agreements** and influence tax policies
- Exploit global markets and **affect economic stability**
- Consider the social and environmental impact of economic decisions

### IV. Security & Diplomacy
- Manage **internal and external security**
- Develop **espionage and cyber defense programs**
- Form **diplomatic alliances** or engage in hostile actions
- Impose **economic sanctions or launch military offensives**

### V. Faction-Specific Actions
Each faction has unique actions adapted to its context:
- Space projects, cyberattacks, media manipulation...
- Specific alliances and influence strategies
- Unique objectives and secret missions

## Game Objective
The goal is to have the most advanced AI while fulfilling your faction's specific objectives. Players must anticipate other factions' reactions, make compromises, and take impactful decisions.

## Installation & Setup
### Prerequisites
- **Node.js** (for the frontend)
- **.NET 8 SDK**
- **SQL Server**

### Project Installation
1. Clone the repository:
   ```bash
    git clone https://github.com/RoiPorci/AiConclave.git
    cd AiConclave
   ```

2. Install the frontend:
    ```bash
    cd Front
    npm install
   ```

3. Install the backend:
    ```bash
    cd ../Back
    dotnet restore
   ```

### Project Installation
1. Start the backend:
    ```bash
    cd backend
    dotnet run
   ```
   
2. Start the frontend:
    ```bash
    cd frontend
    npm run dev
   ```

## License
This project is licensed under the MIT license.