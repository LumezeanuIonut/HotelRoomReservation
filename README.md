# Hotel Room Reservation App

## Overview
The Hotel Room Reservation App is designed to help users manage room availability and search for available rooms in hotels. It provides a console interface for interacting with the Room Management API, allowing users to check room availability and search for rooms based on specific criteria.

## Features
- **Check Room Availability**: Use the `Availability(hotelId, period, roomType)` command to check the availability of rooms in a specific hotel for a given period and room type.
- **Search Room Availability**: Use the `Search(hotelId, numberOfDays, roomType)` command to search for room availability over a specified number of days.

## How to Use
1. Run the program.
2. Enter commands in the console interface:
   - To check room availability, type: `Availability(hotelId, period, roomType)`
     - Example: `Availability(H1, 20250901-20250902, SGL)`
   - To search room availability, type: `Search(hotelId, numberOfDays, roomType)`
     - Example: `Search(H1, 30, SGL)`
3. Type `Exit` to quit the program.

## Notes
- Ensure the Room Management API is running at the specified base address (`http://localhost:5208`).
- The program uses HTTP requests to interact with the API.

---
Created by **Lumezeanu Ionut**
