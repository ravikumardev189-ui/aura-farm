# 🌀 AuraFarm — Aura you can't fake

**A real-time voice AI agent that tries to break your composure in 90 seconds.**
Stay calm and earn aura. Crack — and lose it.

Built for the **Microsoft Agents League 2026** · Creative Apps track · with **GitHub Copilot**.

🎥 **[Watch the demo video](https://youtu.be/azKi2DrrRoM)**

---

## What it is

AuraFarm turns the internet's "aura farming" meme into a live voice game. An AI
agent — "THE AGENT" — greets you, learns a little about you, then playfully roasts
you for 90 seconds, trying to crack your composure. The calmer you stay, the more
aura you earn. The moment your voice wavers, it pounces and your aura drops.

Everything happens through **live, two-way voice** — you talk, it talks back, in
real time.

## How it works

AuraFarm is a genuine multi-step agent loop running every turn:

1. **Perceive** — your microphone audio is analyzed in the browser (pitch + volume
   stability via the Web Audio API) to produce a live composure score.
2. **Reason** — the composure signal is streamed to the agent, which reasons about
   how rattled you sound and decides its next move.
3. **Score** — the agent calls an `updateAura` function to award or deduct aura,
   driving the live UI (popups, feed, total).
4. **Respond** — the agent speaks its next roast, then waits for your reply.

A glowing particle **orb** and a live **composure graph** react to your voice in
real time, so you can *see* yourself staying calm or cracking.

## Tech stack

| Layer | Technology |
|-------|-----------|
| Voice + reasoning | **OpenAI Realtime API** (`gpt-realtime`), speech-to-speech over **WebRTC** |
| Backend | **.NET 9** minimal API — serves the app and mints short-lived ephemeral session tokens |
| Frontend | Single-page HTML/JS — Canvas orb, live chart, agent feed (no framework) |
| Local signal | **Web Audio API** — in-browser pitch/volume analysis for the composure score |
| Built with | **GitHub Copilot** throughout |

## Safety by design

This is a roast game that's kind underneath:

- The agent **only** teases your composure, your words, and your voice — **never**
  your appearance, identity, family, religion, race, or intelligence.
- If a player seems genuinely upset rather than playing, the agent drops character
  and checks on them.
- The OpenAI API key **never reaches the browser** — the .NET backend mints a
  short-lived ephemeral token, and the secret key stays server-side (loaded from
  user-secrets in dev, environment variables in production).

## Running locally

**Requirements:** .NET 9 SDK, an OpenAI API key with Realtime access, a modern
browser (Chrome recommended), and a microphone.

```bash
# 1. Clone
git clone https://github.com/ravikumardev189-ui/aura-farm.git
cd aura-farm

# 2. Add your OpenAI key (kept out of source control)
dotnet user-secrets init
dotnet user-secrets set "OPENAI_API_KEY" "sk-your-key-here"

# 3. Run
dotnet run
```

Then open the URL it prints (e.g. `http://localhost:5283`), click **Test your aura**,
allow the microphone, and try to stay calm.

> Note: microphone access requires `localhost` or HTTPS. When deploying, set
> `OPENAI_API_KEY` as an environment variable and use HTTPS.

## Project structure

```
AuraFarm.Api/
├── Program.cs            # .NET 9 minimal API: serves the app + /api/session token endpoint
├── wwwroot/
│   └── index.html        # the entire single-page app (orb, session, voice client)
├── AuraFarm.Api.csproj
└── appsettings.json
```

## Judging criteria mapping

- **Reasoning & multi-step** — perceive → reason → score → respond loop with function calls
- **Reliability & safety** — content guardrails, graceful error handling, secret never in the browser
- **Creativity** — a novel voice-composure game built on a current meme
- **UX & presentation** — live orb, composure graph, real-time aura, title cards

---

*Built with GitHub Copilot for the Microsoft Agents League 2026.*
