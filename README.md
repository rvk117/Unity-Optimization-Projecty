# Unity Crowd Simulation – Performance Optimization Demo

This project showcases a high-density crowd simulation in Unity with a focus on
**performance engineering**, **AI update optimization**, and **scalable system design**.

The scene contains **20,000+ actively moving agents**, with a toggle that lets you switch
between a **Naive** implementation and an **Optimized** implementation in real time.

---

## Goals of the Project
- Demonstrate understanding of **common performance bottlenecks** in Unity.
- Implement **object pooling** to eliminate expensive Instantiate/Destroy calls.
- Reduce CPU cost using **AI update throttling** (frame-staggering).
- Maintain visual movement speed using **delta time compensation**.
- Provide real-time feedback through **UI FPS and agent counters**.
- Show clear, measurable performance improvements.

---

## Features

### Native/Naive Mode
- Every agent updates **every frame**.
- Full movement logic.
- Uses Instantiate/Destroy for spawning.
- Produces noticeable frame drops at high agent counts.

### Optimized Mode
- Agents spawned using a **pooled object system**.
- AI update cost reduced by **75% or more** using throttled updates.
- DeltaTime compensation keeps movement visually identical.
- Performance improvement from **~24 FPS to ~40 FPS** at 20,000 agents.
- Zero allocations during runtime (no GC spikes)
